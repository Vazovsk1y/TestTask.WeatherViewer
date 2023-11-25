using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using TestTask.Application.Interfaces;
using TestTask.Web.Utils;

namespace TestTask.Web.Pages.Weather;

public class WeatherUploadModel : PageModel
{
    [BindProperty]
    public InputModel Input { get; set; } = new();

    private readonly IWeatherService _weatherService;
    private readonly ILogger<WeatherUploadModel> _logger;
    public WeatherUploadModel(
        IWeatherService weatherService,
        ILogger<WeatherUploadModel> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        string archivesStoragePath = Path.GetTempPath();
        foreach (var file in Input.Files!)
        {
            _logger.LogInformation("Saving archive file at the server {fileName}.", file.FileName);
            string filePath = Path.Combine(archivesStoragePath, $"file.FileName");
            var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Dispose();

            _logger.LogInformation("Parsing xlsx table file starting.");
            await _weatherService.SaveFromTableAsync(filePath);
            System.IO.File.Delete(filePath);
        }

        return RedirectToPage("Index");
    }
    public class InputModel
    {
        [Required(ErrorMessage = "Необходимо выбрать хотя бы один файл.")]
        [AllowedFilesTypes(AllowedExtensions = new string[] { ".xlsx" }, ErrorMessage = "Возможна загрузка только .xlsx файлов.")]
        [RangeCollectionCount(1, 5, ErrorMessage = "Допустимое кол-во файлов для загрузки 5.")]
        public IEnumerable<IFormFile>? Files { get; set; }
    }
}
