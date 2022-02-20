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
        attributes = json.dumps({ "created_at": timestamp, "title": request.title, "description": request.description })

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
    
    @staticmethod
    def create_post_tags(batch_writer, post, tags: List[str]):
        result, errors = [], []
        for tag in tags:
            post_tag = { 
                "PK": f"{PostRepository.prefix}#{post['post_id']}",
                "SK": tag,
                "category": f"{CategoryRepository.prefix}#{post['category']}",
                "attributes": json.dumps(
                    { 
                        "title": post["title"],
                        "description": post["description"], 
                        "created_at": post["created_at"] 
                    })
            }
            try:
                batch_writer.put_item(Item=post_tag)
                result.append(tag)
            except Exception as exception:
                errors.append(f"Error adding tag {tag} to post")
                logger.error(f"Error inserting post tag: {str(exception)}")

        return { "result": result, "errors": errors }
