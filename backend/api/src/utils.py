import json
import os
import boto3
from logging import Logger, INFO, getLogger
from time import time
from typing import Dict

class UserInformation:
    def __init__(self, event):
        claims = event["requestContext"]["authorizer"]["jwt"]["claims"]
        self.username = claims["cognito:username"]
        self.email = claims["email"]
        self.groups = claims["cognito:groups"][1:-1].split(",") 

    @staticmethod
    def extract(event):
        return UserInformation(event)

def lambda_response(status_code: int, headers: Dict, body: Dict) -> Dict:
    headers["Content-Type"] = "application/json"
    headers["Access-Control-Allow-Headers"] = "Content-Type,X-Amz-Date,Authorization,x-requested-with"
    headers["Access-Control-Allow-Origin"] = "*" 
    headers["Access-Control-Allow-Methods"] = "OPTIONS,POST,GET,PUT,DELETE" 
    headers["Access-Control-Allow-Credentials"] = True
    
    return {
        'statusCode': status_code,
        'headers': headers,
        'body': json.dumps(body)
    }

def get_time() -> str:
    return str(time())

def get_logger() -> Logger:
    logger = getLogger()
    logger.setLevel(INFO)
    return logger

def get_dynamodb():
    table_name = os.environ.get("TABLE")
    dynamodb = boto3.resource('dynamodb').Table(table_name)
    return dynamodb

def remove_prefix(s: str) -> str:
    return s[s.find("#") + 1 : ]
    