using System.ComponentModel.DataAnnotations;

namespace TestTask.Web.Validators;

public class MaximumFilesLengthAttribute : ValidationAttribute
{
	private readonly long _maximumFilesLengthBytes;

	public MaximumFilesLengthAttribute(long maximumFilesLengthBytes)
	{
		if (maximumFilesLengthBytes < 0)
		{
			throw new ArgumentException("maximum file length must be non-negative number.");
		}

		_maximumFilesLengthBytes = maximumFilesLengthBytes;
		ErrorMessage = $"Files size limit is {_maximumFilesLengthBytes} bytes.";
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		IFormFile? file = value as IFormFile;
		if (file is not null)
		{
			if (file.Length > _maximumFilesLengthBytes)
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}

		IEnumerable<IFormFile>? files = value as IEnumerable<IFormFile>;
		if (files is not null)
		{
			var sum = files.Select(e => e.Length).Sum();
			if (sum > _maximumFilesLengthBytes)
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}

		return ValidationResult.Success;
	}
}

