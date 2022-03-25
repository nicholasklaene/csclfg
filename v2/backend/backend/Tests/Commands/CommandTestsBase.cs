using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Tests.Commands;

public class CommandTestsBase
{
    protected static List<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, ctx, validationResults, true);
        return validationResults.ToList();
    }
    
    protected static bool ValidationHasErrorWithMessage(List<ValidationResult> validation, string message)
    {
        return validation.Exists(v => v.ErrorMessage?.Contains(message) ?? false);
    }
}
