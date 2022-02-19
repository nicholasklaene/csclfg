from abc import ABCMeta, abstractmethod
from typing import List

class BodyParsingException(Exception):
    def __init__(self, message):            
        super().__init__(message)


class Validation:
    def __init__(self, errors: List[str]):
        self.errors = errors

    
class IRequest:
    __metaclass__ = ABCMeta

    @staticmethod
    @abstractmethod
    def parse(event): raise NotImplementedError

    @abstractmethod
    def validate(self) -> Validation: raise NotImplementedError
