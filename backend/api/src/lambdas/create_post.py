
import json
from data.post import PostRepository
from data.category import CategoryRepository
from models.post import CreatePostRequest
from utils import UserInformation, get_dynamodb, get_logger, lambda_response

logger, dynamodb = get_logger(), get_dynamodb()

def lambda_handler(event, context):
    logger.info(f"Receieved request to create new post:  {json.dumps(event)}")

    parse_result = CreatePostRequest.parse(event)
    if len(parse_result.errors) > 0:
        logger.info(f"Parse failed with errors: {json.dumps(parse_result.errors)}")
        return lambda_response(status_code=400, headers={}, body={ "errors": parse_result.errors })

    create_post_request = parse_result.result
    validation = create_post_request.validate()
    if len(validation.errors) > 0:
        logger.info(f"Validation failed with errors: {json.dumps(validation.errors)}")
        return lambda_response(status_code=400, headers={}, body={ "errors": validation.errors })

    logger.info(f'Checking if category {create_post_request.category} exists')
    result = CategoryRepository.get(create_post_request.category)
    if len(result["errors"]) > 0:
        return lambda_response(status_code=400, headers={}, body={ "errors": result["errors"] })

    user_information = UserInformation.extract(event)
    create_result = PostRepository.create(request=create_post_request, user_information=user_information)
    if len(create_result["errors"]) > 0:
        return lambda_response(status_code=500, headers={}, body={ "errors": result["errors"] })

    create_result = create_result["result"]

    return lambda_response(status_code=201, headers={}, body=create_result)
