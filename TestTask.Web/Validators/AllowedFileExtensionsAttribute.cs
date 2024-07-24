using System.ComponentModel.DataAnnotations;

namespace TestTask.Web.Validators;

public class AllowedFileExtensionsAttribute : ValidationAttribute
{
    public required string[] AllowedExtensions { get; init; }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        switch (value)
        {
            case IFormFile file:
            {
                var extension = Path.GetExtension(file.FileName);
                return !AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase) ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
            }
            default:
                return ValidationResult.Success;
        }
    }
}
