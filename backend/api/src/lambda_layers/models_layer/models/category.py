import json

from models.shared import BodyParsingException, IRequest, Validation

class CreateCategoryRequest(IRequest):
    def __init__(self, label):
        self.label = label

    def validate(self):
        errors = []
        if len(self.label) == 0 or len(self.label) > 100:
            errors.append("Length of label must be between 0 and 100")
        return Validation(errors=errors)

    @staticmethod
    def parse(event):
        try:
            body = json.loads(event["body"])
            return CreateCategoryRequest(**body)
        except Exception as exception:
            raise BodyParsingException("Error parsing body: " + str(exception))
