import json
import math
from uuid import uuid4
from typing import List
from data.user import UserRepository
from data.category import CategoryRepository
from models.post import CreatePostRequest
from utils import UserInformation, get_dynamodb, get_logger, get_time, remove_prefix
from boto3.dynamodb.conditions import Key

logger, dynamodb = get_logger(), get_dynamodb()

class PostRepository:
    prefix = "POST"

    @staticmethod
    def get_bulk(category: str, start: str, end: str, limit: int, tags: List[str]):
        kce = Key("GSI3PK").eq(f"{CategoryRepository.prefix}#{category}") 

        if end:
            kce = kce & Key("GSI3SK").between(end, start)
        else:    
            kce = kce & Key("GSI3SK").lt(start)

        results = dynamodb.query(
            IndexName="GSI3",
            KeyConditionExpression=kce,
            ScanIndexForward = False,
            Limit = limit
        )["Items"]

        posts = []
        for result in results:
            attributes = json.loads(result["attributes"])

            tag_intersection = list(set(tags) & set(attributes["tags"]))
            if len(tags) > 0 and len(tag_intersection) == 0:
                continue

            post = {
                "post_id": remove_prefix(result["PK"]),
                "category": remove_prefix(result["GSI3PK"]),
                "created_at": math.floor(float(result["GSI3SK"])),
                "description": attributes["description"],
                "title": attributes["title"],
                "tags": attributes["tags"]
            }
            posts.append(post)
        
        return posts

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
    