import json
from data.category import CategoryRepository
from utils import get_dynamodb, get_logger, get_time, remove_prefix, lambda_response 
from boto3.dynamodb.conditions import Key

logger, dynamodb = get_logger(), get_dynamodb()

def lambda_handler(event, context):    
    if "queryStringParameters" not in event:
        return lambda_response(status_code=400, headers={}, body={ "errors": "Search paramaters required" })
    
    query_parameters = event["queryStringParameters"]
    if "category" not in query_parameters:
        return lambda_response(status_code=400, headers={}, body={ "errors": "Category required" })
    
    category = query_parameters['category']
    current_time = get_time()

    kce = Key("GSI3PK").eq(f"{CategoryRepository.prefix}#{category}")
    if "start" in query_parameters and "end" in query_parameters:
        kce = kce & Key("GSI3SK").between(query_parameters["start"], query_parameters["end"]) 
    else:
        kce = kce & Key("GSI3SK").lt(current_time)

    results = dynamodb.query(
        IndexName="GSI3",
        KeyConditionExpression=kce,
        ScanIndexForward = False,
        Limit = 10
    )["Items"]

    posts = []
    for result in results:
        attributes = json.loads(result["attributes"])
        post = {
            "post_id": remove_prefix(result["PK"]),
            "category": remove_prefix(result["GSI3PK"]),
            "created_at": result["GSI3SK"],
            "description": attributes["description"],
            "title": attributes["title"]
        }
        posts.append(post)

    return lambda_response(status_code=200, headers={}, body={ "posts": posts })
