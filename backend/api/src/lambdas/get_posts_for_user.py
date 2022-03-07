from data.post import PostRepository
from utils import lambda_response 

def lambda_handler(event, context):
    user_id = event["pathParameters"]["id"]
    posts = PostRepository.get_user_posts(user_id=user_id)
    return lambda_response(200, {}, { "posts": posts })
