import json

from data.category import CategoryRepository
from models.category import CreateCategoryRequest
from utils import get_dynamodb, get_logger, lambda_response

logger, dynamodb = get_logger(), get_dynamodb()

def lambda_handler(event, context):
    logger.info(f"Receieved request to create new category: {json.dumps(event)}")

    parse_result = CreateCategoryRequest.parse(event)
    if len(parse_result.errors) > 0:
        logger.info(f"Parse failed with errors: {json.dumps(parse_result.errors)}")
        return lambda_response(status_code=400, headers={}, body={ "errors": parse_result.errors })
    
    create_category_request = parse_result.result
    validation = create_category_request.validate()
    if len(validation.errors) > 0:
        logger.info(f"Validation failed with errors: {json.dumps(validation.errors)}")
        return lambda_response(status_code=400, headers={}, body={ "errors": validation.errors })

    create_result = CategoryRepository.create(create_category_request)
    if len(create_result["errors"]) > 0:
        return lambda_response(status_code=500, headers={}, body={ "errors": create_result["errors"] })    
    
    return lambda_response(status_code=201, headers={}, body=create_result["result"])
