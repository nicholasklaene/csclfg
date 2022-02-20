
import json

from data.post import PostRepository
from data.category import CategoryRepository
from data.tag import TagRepository
from models.post import CreatePostRequest
from utils import UserInformation, get_dynamodb, get_logger, lambda_response

logger, dynamodb = get_logger(), get_dynamodb()

def lambda_handler(event, context):
    logger.info(f"Receieved request to create new post:  {json.dumps(event)}")

    parse_result = CreatePostRequest.parse()
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
    create_result = PostRepository.create(request=create_post_request, user_infomation=user_information)
    if len(create_result["errors"]) > 0:
        return lambda_response(status_code=500, headers={}, body={ "errors": result["errors"] })

    create_result = create_result["result"]

    created_tags = []
    with dynamodb.batch_writer() as batch_writer:
        tag_create_result = TagRepository.batch_create_tags(batch_writer=batch_writer, tags=create_post_request.tags)
        created_tags = tag_create_result["result"]
        post_tags_create_result = PostRepository.create_post_tags(batch_writer=batch_writer, post=create_result["post_id"], tags=created_tags)
        create_result["tags"] = post_tags_create_result["result"]

    logger.info(f"Post with tags created: {json.dumps(create_result)}")

    return lambda_response(status_code=201, headers={}, body=create_result)
