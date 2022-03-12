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

        result = dynamodb.query(
            KeyConditionExpression=Key("PK").eq(f'{CategoryRepository.prefix}#{name}')
        )["Items"]

        if len(result) == 0:
            errors.append(f"Category {name} does not exist")
        else:
            result = CategoryRepository.extract_category(result[0])
    
        return { "result": result, "errors": errors }

    @staticmethod
    def get_all():
        response = dynamodb.query(
            IndexName="GSI1",
            KeyConditionExpression=Key("GSI1PK").eq("CATEGORIES") & Key("GSI1SK").begins_with("CATEGORY#")
        )
        result = [CategoryRepository.extract_category(item) for item in response["Items"]]
        return { "result": result, "errors": [] }

    @staticmethod
    def create(request: CreateCategoryRequest):
        category_id = f'{CategoryRepository.prefix}#{request.label}'

        suggested_tags = request.suggested_tags if request.suggested_tags != None else []

        data = {
            'PK': category_id,
            'SK': category_id,
            'GSI1PK': "CATEGORIES",
            'GSI1SK': category_id,
            'attributes': json.dumps({
                "suggested_tags": suggested_tags
            })
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

    @staticmethod
    def extract_category(query_result):
        label= remove_prefix(query_result["PK"])
        attributes = json.loads(query_result["attributes"])
        suggested_tags = attributes["suggested_tags"]
        return { "label": label, "suggested_tags": suggested_tags }    

    def update():
        pass
    