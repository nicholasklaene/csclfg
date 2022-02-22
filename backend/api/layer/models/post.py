import json
from typing import List
from models.shared import IRequest, ParseResult, Validation

class CreatePostRequest(IRequest):
    def __init__(self, title: str, description: str, category: str, tags: List[str]):
        self.title = title
        self.description = description
        self.category = category
        self.tags = tags

    def validate(self):
        errors = []
        if len(self.title) == 0 or len(self.title) > 100:
            errors.append("Title must be between 0 and 100 characters")
        if len(self.description) == 0 or len(self.description) > 1024:
            errors.append("Description must be between 0 and 1024 characters")
        if len(self.tags) > 5:
            errors.append("Length of tags must be less than or equal to 5")
        return Validation(errors=errors)

    @staticmethod
    def parse(event):
        errors = []
        if "body" not in event:
            errors.append("Body is required")
            return ParseResult(result=None, errors=errors)

        body = json.loads(event["body"])
        if "title" not in body:
            errors.append("title is required")
        if "description" not in body:
            errors.append("description is required")
        if "category" not in body:
            errors.append("category is required")
        if "tags" not in body:
            errors.append("tags is required")
        
        if len(errors) > 0:
            return ParseResult(result=None, errors=errors)

        try:
            result = CreatePostRequest(**body)
            return ParseResult(result=result, errors=errors)
        except Exception as exception:
            errors.append("Could not parse request body")
            return ParseResult(result=None, errors=errors)
