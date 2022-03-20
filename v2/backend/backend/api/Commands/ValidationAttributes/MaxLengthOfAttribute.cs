using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace api.Commands.ValidationAttributes;

public class MaxLengthOfAttribute : ValidationAttribute
{
    private readonly int _max;
    public MaxLengthOfAttribute(int max)
    {
        _max = max;
    }

    public override bool IsValid(object value)
    {
        var list = value as IList;
        if (list != null)
        {
            return list.Count <= _max;
        }
        return false;
    }
}
