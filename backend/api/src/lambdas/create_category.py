import json
import logging
import os
import boto3
import uuid
from models.category import CreateCategoryRequest
from models.shared import BodyParsingException
from utils.lambda_response import lambda_response

logger = logging.getLogger()
logger.setLevel(logging.INFO)

dynamodb = boto3.resource('dynamodb').Table(os.environ.get("TABLE"))

def lambda_handler(event, context):
    logger.info("Receieved request to create new category: " + json.dumps(event))

    create_category_request = None
    try:
        create_category_request = CreateCategoryRequest.parse(event)
    except BodyParsingException as exception:
        logger.error("Error parsing request body: " + str(exception))
        body = { "errors": ["Could not parse request body"] }
        return lambda_response(status_code=400, headers={}, body=body)
    
    logger.info("Successfully parsed request body")

    validation = create_category_request.validate()
    if len(validation.errors) > 0:
        logger.info("Invalid request body: " + ", ".join(validation.errors))
        return lambda_response(status_code=400, headers={}, body={ "errors": validation.errors })

    category_id = f'CATEGORY#{uuid.uuid4().hex}'
    data = {
        'PK': category_id,
        'SK': category_id,
        'name': create_category_request.label
    }

    logger.info('Inserting data: ' + json.dumps(data))

    try:
        dynamodb.put_item(Item=data)
        logger.info('Inserted data: ' + json.dumps(data))
    except Exception as exception:
        logger.error('Error inserting data: ' + str(exception))
        body = { "errors": ["Error creating category"]}
        return lambda_response(status_code=500, headers={}, body=body)

    body = { 
        "category_id": category_id.split("#")[-1],
        "label": create_category_request.label
    }

    return lambda_response(status_code=201, headers={}, body=body)
