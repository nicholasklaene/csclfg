class UserInformation:
    def __init__(self, event):
        claims = event["requestContext"]["authorizer"]["jwt"]["claims"]
        self.email = claims["email"]
        self.groups = claims["cognito:groups"][1:-1].split(",") 


def extract_claims(event) -> UserInformation:
    user = UserInformation(event)
    return user

