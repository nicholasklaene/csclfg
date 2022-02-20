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
    start = query_parameters["start"] if "start" in query_parameters else get_time()
    limit = min(100, int(query_parameters["limit"])) if "limit" in query_parameters else 10

    kce = Key("GSI3PK").eq(f"{CategoryRepository.prefix}#{category}") & Key("GSI3SK").lt(start)

    results = dynamodb.query(
        IndexName="GSI3",
        KeyConditionExpression=kce,
        ScanIndexForward = False,
        Limit = limit
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
