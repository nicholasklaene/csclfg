using System.ComponentModel.DataAnnotations;

namespace Api.ValidationAttributes;

public class IdAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        
        return Convert.ToInt32(value) > 0 
            ? ValidationResult.Success 
            : new ValidationResult($"{validationContext.DisplayName} is required");
    }
}