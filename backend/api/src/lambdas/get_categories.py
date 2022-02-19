import logging
import boto3
import os
from boto3.dynamodb.conditions import Key
from utils.lambda_response import lambda_response

logger = logging.getLogger()
logger.setLevel(logging.INFO)
dynamodb = boto3.resource('dynamodb').Table(os.environ.get("TABLE"))

def lambda_handler(event, context):
    logger.info("Recieved request to get all categories")
    try:
        response = dynamodb.query(
            IndexName="GSI1",
            KeyConditionExpression=Key("GSI1PK").eq("CATEGORIES") & Key("GSI1SK").begins_with("CATEGORY#")
        )
        logger.info("Retrieved categories from dynamo")
        categories = [{"label": item["PK"].split("#")[1]} for item in response["Items"]]
        return lambda_response(status_code=200, headers={}, body={ "categories": categories })
    except Exception as exception:
        logger.error("Error getting categories: " + str(exception))
        return lambda_response(status_code=500, headers={}, body={ "errors": ["Error retrieving categories"] })
