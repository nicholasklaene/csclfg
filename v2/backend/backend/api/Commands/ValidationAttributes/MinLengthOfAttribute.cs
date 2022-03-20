using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace api.Commands.ValidationAttributes;

public class MinLengthOfAttribute : ValidationAttribute
{
    private readonly int _min;
    public MinLengthOfAttribute(int min)
    {
        _min = min;
    }

    public override bool IsValid(object value)
    {
        var list = value as IList;
        if (list != null)
        {
            return list.Count >= _min;
        }
        return false;
    }
}
