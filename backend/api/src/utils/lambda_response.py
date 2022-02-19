import json
from typing import Dict

def lambda_response(status_code: int, headers: Dict, body: Dict):
    return {
        'statusCode': status_code,
        'headers': headers,
        'body': json.dumps(body)
    }
