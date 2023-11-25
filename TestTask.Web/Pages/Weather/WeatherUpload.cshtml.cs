using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TestTask.Application.Interfaces;
using TestTask.Web.Utils;

namespace TestTask.Web.Pages.Weather;

public class WeatherUploadModel : PageModel
{
    [BindProperty]
    public BindingModel BindingEntity { get; set; } = new();

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
        var uploadResults = new List<FileUploadResultViewModel>();
        foreach (var file in BindingEntity.Files!)
        {
            _logger.LogInformation("Saving archive file at the server {fileName}.", file.FileName);
            string filePath = Path.Combine(archivesStoragePath, $"file.FileName");
            var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Dispose();

            _logger.LogInformation("Parsing xlsx table file starting.");
            var result = await _weatherService.SaveFromTableAsync(filePath);
            uploadResults.Add(new FileUploadResultViewModel(file.FileName, result.IsSuccess));
            System.IO.File.Delete(filePath);
        }

        BindingEntity.UploadResults = uploadResults;
        return Page();
    }
    public class BindingModel
    {
        [Required(ErrorMessage = "Необходимо выбрать хотя бы один файл.")]
        [AllowedFilesTypes(AllowedExtensions = new string[] { ".xlsx" }, ErrorMessage = "Возможна загрузка только .xlsx файлов.")]
        [RangeCollectionCount(1, 5, ErrorMessage = "Допустимое кол-во файлов для загрузки 5.")]
        public IEnumerable<IFormFile>? Files { get; set; }

        public IEnumerable<FileUploadResultViewModel>? UploadResults { get; set; }
    }
}
public record FileUploadResultViewModel(string FileName, bool SuccessfullyParsed);

