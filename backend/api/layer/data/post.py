import json
from uuid import uuid4
from typing import List
from data.user import UserRepository
from data.category import CategoryRepository
from models.post import CreatePostRequest
from utils import UserInformation, get_dynamodb, get_logger, get_time, remove_prefix

logger, dynamodb = get_logger(), get_dynamodb()

class PostRepository:
    prefix = "POST"

    @staticmethod
    def create(request: CreatePostRequest, user_information: UserInformation):
        category_id = f'{CategoryRepository.prefix}#{request.category}'
        post_id = f'{PostRepository.prefix}#{uuid4().hex}'
        user_id = f'{UserRepository.prefix}#{user_information.email}'
        timestamp = get_time()
        
        attributes = json.dumps({ 
            "created_at": timestamp, 
            "title": request.title,
            "description": request.description,
            "tags": request.tags, 
        })

        post_data = {
            "PK": post_id,
            "SK": post_id,
            "GSI1PK": user_id,
            "GSI1SK": post_id,
            "GSI2PK": category_id,
            "GSI2SK": post_id,
            "GSI3PK": category_id,
            "GSI3SK": timestamp,
            "attributes": attributes
        }

        logger.info(f"Inserting post: {json.dumps(post_data)}")

        result, errors = None, []
        try:
            dynamodb.put_item(Item=post_data)
            result = {
                "post_id": remove_prefix(post_id),
                "category": request.category,
                "created_at": timestamp,
                "description": request.description,
                "title": request.title
            }
        except Exception as exception:
            logger.error(f"Error inserting post:  {str(exception)}")
            errors.append("Error inserting post")

        return { "result": result, "errors": errors }
    