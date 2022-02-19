
import json
import logging
import os
import uuid
import boto3
from time import time
from boto3.dynamodb.conditions import Key

from utils.extract_claims import extract_claims
from utils.lambda_response import lambda_response
from requests.create_post_request import CreatePostRequest
from requests.shared import BodyParsingException

logger = logging.getLogger()
logger.setLevel(logging.INFO)

dynamodb = boto3.resource('dynamodb').Table(os.environ.get("TABLE"))

def lambda_handler(event, context):
    logger.info("Receieved request to create new post: " + json.dumps(event))

    create_post_request = None
    try:
        create_post_request = CreatePostRequest.parse(event)
        logger.info("Successfully parsed request body")
    except BodyParsingException as exception:
        logger.error("Error parsing request body: " + str(exception))
        body = { "errors": ["Could not parse request body"] }
        return lambda_response(status_code=400, headers={}, body=body)

    validation = create_post_request.validate()
    if len(validation.errors) > 0:
        logger.info("Invalid request body: " + ", ".join(validation.errors))
        return lambda_response(status_code=400, headers={}, body={ "errors": validation.errors })

    try:
        logger.info(f'Checking if category {create_post_request.category} exists')
        dynamodb.query(KeyConditionExpression=Key("PK").eq(f'CATEGORY#{create_post_request.category}'))["Items"][0]
    except Exception as exception:
        logger.error(f'Error retrieving category {create_post_request.category}')
        return lambda_response(status_code=400, headers={}, body={ "errors": [f'Category {create_post_request.category} does not exist'] })

    category_id = f'CATEGORY#{create_post_request.category}'
    user_id = f'USER#{extract_claims(event).email}'
    post_id = f'POST#{uuid.uuid4().hex}'
    timestamp = time()

    post_data = {
        "PK": post_id,
        "SK": post_id,
        "GSI1PK": user_id,
        "GSI1SK": post_id,
        "GSI2PK": category_id,
        "GSI2SK": post_id,
        "created_at": timestamp,
        "title": create_post_request.title,
        "description": create_post_request.description
    }

    # All or nothing, cannot be part of batch write
    logger.info("Inserting post: " + json.dumps(post_data))
    try:
        dynamodb.put_item(Item=post_data)
        logger.info("Successfully inserted post")
    except Exception as exception:
        logger.error("Error inserting post")
        return lambda_response(status_code=400, headers={}, body={ "errors": ["Error inserting post"]})
    
    
    with dynamodb.batch_writer() as batch_writer:
        tags = [{ "PK": f'TAG#{tag}', "SK": f'TAG#{tag}' } for tag in create_post_request.tags]
        insert_tags(batch_writer, tags)
            
        post_tags = [{ 
            "PK": post_id,
            "SK": tag,
            "category": create_post_request.category,
            "created_at": timestamp,
            "title": create_post_request.title,
            "description": create_post_request.description
            } for tag in tags]
        insert_post_tags(batch_writer, post_tags)


    new_post = {
        "post_id": post_id[post_id.find("#") + 1 : ],
        "category": create_post_request.category,
        "created_at": timestamp,
        "description": create_post_request.description,
        "title": create_post_request.title
    }

    return lambda_response(status_code=201, headers={}, body=new_post)

def insert_tags(batch_writer, tags):
    logger.info("Inserting tags: " + json.dumps(tags))
    for tag in tags:
        try:
            logger.info("Inserting tag: " + json.dumps(tag))
            batch_writer.put_item(Item=tag)
        except Exception as exception:
            logger.error("Error inserting tag")

def insert_post_tags(batch_writer, post_tags):
    logger.info("Inserting post tags: " + json.dumps(post_tags))
    for post_tag in post_tags:
        try:
            logger.info("Inserting post tag: " + json.dumps(post_tag))
            batch_writer.put_item(Item=post_tag)
        except Exception as exception:
            logger.error("Error inserting post tag")