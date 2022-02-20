from abc import ABCMeta, abstractmethod
from typing import List

class Validation:
    def __init__(self, errors: List[str]):
        self.errors = errors


class ParseResult:
    def __init__(self, result, errors: List[str]):
        self.result = result
        self.errors = errors
    

class IRequest:
    __metaclass__ = ABCMeta

    @staticmethod
    @abstractmethod
    def parse(event) -> ParseResult: raise NotImplementedError

    @abstractmethod
    def validate(self) -> Validation: raise NotImplementedError
