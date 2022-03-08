import logging
import boto3

logger = logging.getLogger()
logger.setLevel(logging.INFO)

cognito = boto3.client('cognito-idp')

def lambda_handler(event, context):
    """
    Post confirmation lambda trigger
    Adds the new to the 'user' group
    """
    logger.info("Post confirmation trigger running")
    username = event["userName"]
    logger.info(f"Adding user {username} to user group")
    cognito.admin_add_user_to_group(UserPoolId=event["userPoolId"], Username=username, GroupName="user")
    logger.info(f"Added user {username} to user group")
    return event
