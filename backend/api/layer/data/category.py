import json
from boto3.dynamodb.conditions import Key
from models.category import CreateCategoryRequest
from utils import get_logger, get_dynamodb, remove_prefix

logger, dynamodb = get_logger(), get_dynamodb()

class CategoryRepository:
    prefix = "CATEGORY"
    
    @staticmethod
    def get(name: str):
        result, errors = None, []
        try:
            result = dynamodb.query(
                KeyConditionExpression=Key("PK").eq(f'{CategoryRepository.prefix}#{name}')
            )["Items"]

            if len(result) == 0:
                errors.append(f"Category {name} does not exist")
            else:
                result = result[0]
        
        except Exception as exception:
            logger.error(f"Error getting category: {str(exception)}")
            errors.append(f"Could not retrieve category {name}")
            
        return { "result": result, "errors": errors }

    @staticmethod
    def get_all():
        result, errors = None, []
        try:
            response = dynamodb.query(
                IndexName="GSI1",
                KeyConditionExpression=Key("GSI1PK").eq("CATEGORIES") & Key("GSI1SK").begins_with("CATEGORY#")
            )
            result = [{"label": remove_prefix(item["PK"])} for item in response["Items"]]
        except Exception as exception:
            logger.error(f"Error getting categories: {str(exception)}")

        return { "result": result, "errors": errors }


    @staticmethod
    def create(request: CreateCategoryRequest):
        category_id = f'{CategoryRepository.prefix}#{request.label}'

        data = {
            'PK': category_id,
            'SK': category_id,
            'GSI1PK': "CATEGORIES",
            'GSI1SK': category_id,
        }

        logger.info(f"Inserting data: {json.dumps(data)}")
        
        body, errors = None, []
        try:
            dynamodb.put_item(Item=data)
            body = { "label": request.label }
        except Exception as exception:
            logger.error(f"Error inserting data: {str(exception)}")
            errors.append("Error creating category")

        return { "result": body, "errors": errors }

    def update():
        pass
    