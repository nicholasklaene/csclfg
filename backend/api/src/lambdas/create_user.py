from utils import UserInformation, lambda_response
from data.user import UserRepository

def lambda_handler(event, context):
    user_information = UserInformation.extract(event)
    create_result = UserRepository.create(user_information)
    
    if len(create_result["errors"]) > 0:
        return lambda_response(500, {}, { "errors": create_result["errors"] })

    return lambda_response(201, {}, {})
