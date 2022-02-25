from data.post import PostRepository
from utils import get_time, lambda_response 

def lambda_handler(event, context):    
    if "queryStringParameters" not in event:
        return lambda_response(status_code=400, headers={}, body={ "errors": "Search paramaters required" })
    
    query_parameters = event["queryStringParameters"]
    if "category" not in query_parameters:
        return lambda_response(status_code=400, headers={}, body={ "errors": "Category required" })
    
    category = query_parameters['category']
    start = query_parameters["start"] if "start" in query_parameters else get_time()
    tags = query_parameters["tags"].split(",") if "tags" in query_parameters else []
    end = query_parameters["end"] if "end" in query_parameters else None
    
    posts = PostRepository.get_bulk(category=category, start=start, end=end, limit=10, tags=tags)

    return lambda_response(status_code=200, headers={}, body={ "posts": posts })
