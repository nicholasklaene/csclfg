import json
import logging
from utils.extract_claims import extract_claims 

logger = logging.getLogger()
logger.setLevel(logging.INFO)

def lambda_handler(event, context):
    claims = extract_claims(event)
    return json.dumps({ "email": claims.email, "groups": claims.groups})
    