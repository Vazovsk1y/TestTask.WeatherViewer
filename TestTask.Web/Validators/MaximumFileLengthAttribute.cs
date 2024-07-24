using System.ComponentModel.DataAnnotations;

namespace TestTask.Web.Validators;

public class MaximumFileLengthAttribute : ValidationAttribute
{
	// bytes
	private readonly long _maximumFilesLength;

	public MaximumFileLengthAttribute(long maximumFilesLength)
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
		return value switch
		{
			IFormFile file when file.Length > _maximumFilesLength => new ValidationResult(ErrorMessage),
			IFormFile file => ValidationResult.Success,
			_ => ValidationResult.Success
		};
	}
}

