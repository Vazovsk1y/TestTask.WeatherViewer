using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Web.Utils;

public class RangeCollectionCountAttribute : ValidationAttribute
{
	public int From { get; }

    public int To { get; }

	public RangeCollectionCountAttribute(int from, int to)
	{
        if (from < 0 || to < 0)
        {
            throw new ArgumentException("from and to value must be non-negative numbers.");
        }

		From = from;
		To = to;
        ErrorMessage = $"Collection count must be from {from} to {to}.";
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		var enumerable = value as IEnumerable;

		if (enumerable is not null)
		{
			if (enumerable is ICollection icollection)
			{
				var count = icollection.Count;
				if (count < From || count > To)
				{
					return new ValidationResult(ErrorMessage);
				}
			}
			else
			{
				var count = enumerable.Cast<object>().Count();
				if (count < From || count > To)
				{
					return new ValidationResult(ErrorMessage);
				}
			}
		}

		return ValidationResult.Success;
	}
}