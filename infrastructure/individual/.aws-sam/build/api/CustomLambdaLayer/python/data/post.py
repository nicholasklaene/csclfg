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
    def create(request: CreatePostRequest, user_information: UserInformation):
        category_id = f'{CategoryRepository.prefix}#{request.category}'
        post_id = f'{PostRepository.prefix}#{uuid4().hex}'
        user_id = f'{UserRepository.prefix}#{user_information.username}'
        timestamp = get_time()
        
        attributes = json.dumps({ 
            "created_at": timestamp, 
            "title": request.title,
            "preview": request.preview,
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
                "preview": request.preview,
                "description": request.description,
                "title": request.title
            }
        except Exception as exception:
            logger.error(f"Error inserting post:  {str(exception)}")
            errors.append("Error inserting post")

        return { "result": result, "errors": errors }

    @staticmethod
    def get_posts(category: str, start: str, end: str, limit: int, tags: List[str]):
        kce = Key("GSI3PK").eq(f"{CategoryRepository.prefix}#{category}")         
        kce = kce & Key("GSI3SK").between(end, start) if end else kce & Key("GSI3SK").lt(start)

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
            
            post = PostRepository.extract_post(result)
            posts.append(post)
        
        return posts
    
    @staticmethod
    def get_posts_by_user(user_id: str):
        kce = Key("GSI1PK").eq(f"{UserRepository.prefix}#{user_id}")
        kce = kce & Key("GSI1SK").begins_with(f"{PostRepository.prefix}#") 
        
        results = dynamodb.query(
            IndexName="GSI1",
            KeyConditionExpression=kce,
        )["Items"]

        posts = [ PostRepository.extract_post(result) for result in results ]

        return posts

    @staticmethod
    def get_post_by_id(post_id: str):
        kce = Key("PK").eq(f"{PostRepository.prefix}#{post_id}")
        kce = kce & Key("SK").eq(f"{PostRepository.prefix}#{post_id}") 
        
        results = dynamodb.query(
            KeyConditionExpression=kce,
            Limit = 1
        )["Items"]

        if len(results) == 0:
            return None
        
        post = PostRepository.extract_post(results[0]) 
        return post

    @staticmethod
    def extract_post(query_result):
        attributes = json.loads(query_result["attributes"])
        post = {
            "post_id": remove_prefix(query_result["PK"]),
            "user_id": remove_prefix(query_result["GSI1PK"]),
            "category": remove_prefix(query_result["GSI3PK"]),
            "created_at": math.floor(float(query_result["GSI3SK"])),
            "preview": attributes["preview"],
            "description": attributes["description"],
            "title": attributes["title"],
            "tags": attributes["tags"]
        }
        return post
