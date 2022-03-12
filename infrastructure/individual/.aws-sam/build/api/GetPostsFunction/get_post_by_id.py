from data.post import PostRepository
from utils import lambda_response 

def lambda_handler(event, context):
    post_id = event["pathParameters"]["id"]
    post = PostRepository.get_post_by_id(post_id=post_id)

    if not post:
        return lambda_response(404, {}, { "errors": f"post {post_id} not found"})    

    return lambda_response(200, {}, { "post": post })
