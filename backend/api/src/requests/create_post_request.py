import json
from typing import List
from shared import IRequest, Validation, BodyParsingException

class CreatePostRequest(IRequest):
    def __init__(self, title: str, description: str, category: str, tags: List[str]):
        self.title = title
        self.description = description
        self.category = category
        self.tags = tags

    # Does not validate whether a category exists
    def validate(self):
        errors = []
        if len(self.title) == 0 or len(self.title) > 100:
            errors.append("Title must be between 0 and 100 characters")
        if len(self.description) == 0 or len(self.description) > 1000:
            errors.append("Description must be between 0 and 1000 characters")
        if len(self.tags) > 5:
            errors.append("Length of tags must be less than or equal to 5")
        return Validation(errors=errors)

    @staticmethod
    def parse(event):
        try:
            body = json.loads(event["body"])
            return CreatePostRequest(**body)
        except Exception as exception:
            raise BodyParsingException("Error parsing body: " + str(exception))

