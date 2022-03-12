from data.category import CategoryRepository
from utils import get_logger, lambda_response

logger = get_logger()

def lambda_handler(event, context):
    logger.info("Recieved request to get all categories")

    get_categories_result = CategoryRepository.get_all()

    if len(get_categories_result["errors"]) > 0:
        return lambda_response(status_code=500, headers={}, body={ "errors": get_categories_result["errors"] })

    return lambda_response(status_code=200, headers={}, body={ "categories": get_categories_result["result"] })
