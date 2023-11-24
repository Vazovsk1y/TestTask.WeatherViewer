using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Web.Pages;

public class WeatherUploadModel : PageModel
{
	[BindProperty]
	public InputModel Input { get; set; } = new();
	public void OnGet()
	{
	}

	public IActionResult OnPost()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		// TODO parsing logic with loading indicator

		return Page();
	}
	public class InputModel
	{
		[Required(ErrorMessage = "Необходимо выбрать хотя бы один файл.")]
		[AllowedFilesTypes(AllowedExtensions = new string[] { ".xlsx" }, ErrorMessage = "Возможна загрузка только .xlsx файлов.")]
		public IEnumerable<IFormFile>? Files { get; set; }
	}
}

public class AllowedFilesTypesAttribute : ValidationAttribute
{
	public required string[] AllowedExtensions { get; init; }

	public AllowedFilesTypesAttribute()
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
