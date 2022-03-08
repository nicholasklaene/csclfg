import json
import logging

logger = logging.getLogger()
logger.setLevel(logging.INFO)

def lambda_handler(event, context):
    """
    Pre token generation lambda trigger
    Adds the correct scopes to the user's JWT
    """
    logger.info("Pre token generation trigger running")

    def map_scopes(item):
        return f'{item}-{event["callerContext"]["clientId"]}'
    
    scopes = " ".join(map(map_scopes, event["request"]["groupConfiguration"]["groupsToOverride"]))
    logger.info("Mapped scopes: " + json.dumps(scopes))

    event["response"] = {
        "claimsOverrideDetails": {
            "claimsToAddOrOverride": {
                "scope": scopes
            }
        }
    }
    
    return event
