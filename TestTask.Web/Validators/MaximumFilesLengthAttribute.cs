using System.ComponentModel.DataAnnotations;

namespace TestTask.Web.Validators;

public class MaximumFilesLengthAttribute : ValidationAttribute
{
	// bytes
	private readonly long _maximumFilesLength;

	public MaximumFilesLengthAttribute(long maximumFilesLength)
	{
		if (maximumFilesLength < 0)
		{
			throw new ArgumentException("Maximum file length must be non-negative number.");
		}

		_maximumFilesLength = maximumFilesLength;
		ErrorMessage = $"Files size limit is {_maximumFilesLength} bytes.";
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		IFormFile? file = value as IFormFile;

		if (file is not null)
		{
			if (file.Length > _maximumFilesLength)
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}

		IEnumerable<IFormFile>? files = value as IEnumerable<IFormFile>;
		if (files is not null)
		{
			var sum = files.Select(e => e.Length).Sum();
			if (sum > _maximumFilesLength)
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}

		return ValidationResult.Success;
	}
}

