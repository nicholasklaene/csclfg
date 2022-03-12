from utils import UserInformation, get_dynamodb, get_logger

dynamodb, logger = get_dynamodb(), get_logger()

class UserRepository:
    prefix = "USER"
    
    @staticmethod
    def create(user_information: UserInformation):
        username = user_information.username
        user_id = f"{UserRepository.prefix}#{username}"

        data = {
            'PK': user_id,
            'SK': user_id,
        }

        logger.info(f"Inserting user {username}")
        
        errors = []
        try:
            dynamodb.put_item(Item=data)
        except Exception as exception:
            message = f"Error inserting user {username}"
            errors.append(message)
            logger.error(message)
            logger.error(str(exception))

        return { "errors": errors }
