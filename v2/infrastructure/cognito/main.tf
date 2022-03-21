terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 3.27"
    }
  }
}

# Lambda zip file
data "archive_file" "lambda_zip" {
  type        = "zip"
  source_dir  = "triggers"
  output_path = "triggers.zip"
}

# IAM Policies & Roles
data "aws_iam_policy_document" "lambda_trust_policy" {
  statement {
    actions = ["sts:AssumeRole"]
    effect  = "Allow"
    principals {
      type        = "Service"
      identifiers = ["lambda.amazonaws.com"]
    }
  }
}

resource "aws_iam_role" "trigger_role" {
  name               = "terraform_function_role"
  assume_role_policy = data.aws_iam_policy_document.lambda_trust_policy.json
}

resource "aws_iam_role_policy_attachment" "trigger_policy" {
  role       = aws_iam_role.trigger_role.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AWSLambdaBasicExecutionRole"
}

# Lambda triggers
resource "aws_lambda_function" "pre_sign_up_trigger" {
  function_name    = "studyseeking_pre_sign_up_trigger"
  filename         = "triggers.zip"
  role             = aws_iam_role.trigger_role.arn
  source_code_hash = data.archive_file.lambda_zip.output_base64sha256
  handler          = "preSignup.handler"
  runtime          = "nodejs14.x"
}

resource "aws_lambda_function" "pre_token_generation" {
  function_name    = "studyseeking_pre_token_generation"
  filename         = "triggers.zip"
  role             = aws_iam_role.trigger_role.arn
  source_code_hash = data.archive_file.lambda_zip.output_base64sha256
  handler          = "preTokenGeneration.handler"
  runtime          = "nodejs14.x"
}


# User pool
resource "aws_cognito_user_pool" "user_pool" {
  name                     = "studyseeking_user_pool"
  auto_verified_attributes = ["email"]
  mfa_configuration        = "OFF"
  schema {
    name                     = "email"
    attribute_data_type      = "String"
    developer_only_attribute = false
    mutable                  = true
    required                 = true
  }
  password_policy {
    minimum_length    = 8
    require_lowercase = true
    require_uppercase = true
    require_numbers   = true
    require_symbols   = false
  }
  lambda_config {
    pre_sign_up          = aws_lambda_function.pre_sign_up_trigger.arn
    pre_token_generation = aws_lambda_function.pre_token_generation.arn
  }
}

# Allow cognito to execute triggers
resource "aws_lambda_permission" "allow_pre_sign_up_execution_from_user_pool" {
  statement_id  = "AllowExecutionFromUserPool"
  action        = "lambda:InvokeFunction"
  function_name = aws_lambda_function.pre_sign_up_trigger.function_name
  principal     = "cognito-idp.amazonaws.com"
  source_arn    = aws_cognito_user_pool.user_pool.arn
}

resource "aws_lambda_permission" "allow_pre_token_generation_execution_from_user_pool" {
  statement_id  = "AllowExecutionFromUserPool"
  action        = "lambda:InvokeFunction"
  function_name = aws_lambda_function.pre_token_generation.function_name
  principal     = "cognito-idp.amazonaws.com"
  source_arn    = aws_cognito_user_pool.user_pool.arn
}

# User pool client
resource "aws_cognito_user_pool_client" "user_pool_client" {
  name                         = "studyseeking_user_pool_client"
  user_pool_id                 = aws_cognito_user_pool.user_pool.id
  generate_secret              = false
  explicit_auth_flows          = ["ALLOW_USER_SRP_AUTH", "ALLOW_REFRESH_TOKEN_AUTH"]
  supported_identity_providers = ["COGNITO"]
  access_token_validity        = 1
  id_token_validity            = 1
  refresh_token_validity       = 1
  token_validity_units {
    access_token  = "hours"
    id_token      = "hours"
    refresh_token = "days"
  }
}

# User groups
resource "aws_cognito_user_group" "admin_group" {
  name         = "admin"
  user_pool_id = aws_cognito_user_pool.user_pool.id
  precedence   = 0
}

resource "aws_cognito_user_group" "user_group" {
  name         = "user"
  user_pool_id = aws_cognito_user_pool.user_pool.id
  precedence   = 10
}
