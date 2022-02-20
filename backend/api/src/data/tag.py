from typing import List
from utils import get_logger, get_dynamodb

logger, dynamodb = get_logger(), get_dynamodb()

class TagRepository:
    prefix = "TAG"

    @staticmethod
    def batch_create_tags(batch_writer, tags: List[str]):
        result, errors = [], []
        for tag in tags:
            tag_data = { "PK": f'TAG#{tag}', "SK": f'TAG#{tag}' }
            try:
                batch_writer.put_item(Item=tag_data)
                result.append(tag)
            except Exception as exception:
                errors.append(f"Error inserting tag: {tag}")
                logger.error(f"Error inserting tag {tag_data}: {str(exception)}")

        return { "result": result, "errors": errors }
