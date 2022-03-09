app_name="studyseeking"
aws_region="us-east-1"

readarray -t cognito_conf < "../config/cognito.txt"

user_pool_id=${cognito_conf[0]}
user_pool_client_id=${cognito_conf[1]}

read -p "App subdomain: " subdomain
app_domain="$subdomain.studyseeking.com"

samconfig="
version = 0.1
[default]
[default.deploy]
[default.deploy.parameters]
stack_name = \"$subdomain-$app_name-infra\"
s3_bucket = \"$app_name.templates\"
s3_prefix = \"$subdomain-$app_name-infra\"
region = \"$aws_region\"
confirm_changeset = false
capabilities = \"CAPABILITY_AUTO_EXPAND CAPABILITY_IAM\"
"

samconfig+="parameter_overrides = \"AppName=\\\""
samconfig+="$subdomain-$app_name"
samconfig+="\\\""
samconfig+=", "

samconfig+="AppDomain=\\\""
samconfig+="$app_domain"
samconfig+="\\\""
samconfig+=", "

samconfig+="UserPoolId=\\\""
samconfig+="$user_pool_id"
samconfig+="\\\""
samconfig+=", "

samconfig+="UserPoolClientId=\\\""
samconfig+="$user_pool_client_id"
samconfig+="\\\""
samconfig+="\""

identity_samconfig+=$'\nimage_repositories = []'

echo "$samconfig" > "../config/sam/$subdomain-samconfig.toml"
