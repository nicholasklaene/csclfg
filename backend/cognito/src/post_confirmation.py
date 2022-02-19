import json
import logging
import boto3
import os

logger = logging.getLogger()
logger.setLevel(logging.INFO)
cognito = boto3.client('cognito-idp')
dynamodb = boto3.resource('dynamodb').Table(os.environ.get("TABLE"))

def lambda_handler(event, context):
    """
    Post confirmation lambda trigger
    Adds the newly confirmed user to DynamoDB
    """

    logger.info("Post confirmation trigger running.")
    logger.info("Recieved event: " + json.dumps(event))

    try:
        email = event["request"]["userAttributes"]["email"]

        user_id = f'USER#{email}'
        data = {
            'PK': user_id,
            'SK': user_id
        }

        logger.info('Inserting user: ' + json.dumps(data))

        dynamodb.put_item(Item=data)
        logger.info('Inserted data: ' + json.dumps(data))
        
    except Exception as exception:
        logger.error('Error inserting user: ' + str(exception))

    try:
        logger.info("Adding user to Users group")
        username = event["request"]["userAttributes"]["username"]
        cognito.admin_add_user_to_group(UserPoolId=os.environ.get("USER_POOL"), Username=username, GroupName="Users")
    except Exception as exception:
        logger.error("Error adding user to users group: " + str(exception))

    logger.info("Returning event: " + json.dumps(event))
    
    return event
