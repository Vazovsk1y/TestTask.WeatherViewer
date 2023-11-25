using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Web.Utils;

public class AllowedFilesExtensionsAttribute : ValidationAttribute
{
    public required string[] AllowedExtensions { get; init; }

    public AllowedFilesExtensionsAttribute()
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        IFormFile? file = value as IFormFile;
        if (file is not null)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        IEnumerable<IFormFile>? files = value as IEnumerable<IFormFile>;
        if (files is not null)
        {
            var badFile = files.FirstOrDefault(e => !AllowedExtensions.Contains(Path.GetExtension(e.FileName)));
            if (badFile is not null)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        return ValidationResult.Success;
    }

    public string GetErrorMessage()
    {
        return string.IsNullOrWhiteSpace(ErrorMessage) ? $"Допустимые расширения файлов: {string.Join(", ", AllowedExtensions)}" : ErrorMessage;
    }
}
