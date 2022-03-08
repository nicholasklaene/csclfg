import json
import logging

logger = logging.getLogger()
logger.setLevel(logging.INFO)

def lambda_handler(event, context):
    """
    Pre token generation lambda trigger
    Adds the correct scopes to the user's JWT
    """

    logger.info("Pre token generation trigger running.")
    logger.info("Recieved event: " + json.dumps(event))

    def map_scopes(item):
        return f'{item}-{event["callerContext"]["clientId"]}'
    
    new_scopes = " ".join(map(map_scopes, event["request"]["groupConfiguration"]["groupsToOverride"]))
    logger.info("New scopes: " + json.dumps(new_scopes))

    event["response"] = {
        "claimsOverrideDetails": {
            "claimsToAddOrOverride": {
                "scope": new_scopes
            }
        }
    }

    logger.info("Returning event: " + json.dumps(event))
    
    return event
