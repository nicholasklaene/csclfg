import json
from utils import UserInformation, get_logger 

logger = get_logger()

def lambda_handler(event, context):
    claims = UserInformation.extract(event)
    return json.dumps({ "email": claims.email, "groups": claims.groups})
    