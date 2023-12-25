using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TestTask.Application.Interfaces;
using TestTask.Web.Validators;

namespace TestTask.Web.Pages.Weather;

public class UploadModel : PageModel
{
    [BindProperty]
    public BindingModel BindingEntity { get; set; } = new();

    private readonly IWeatherService _weatherService;
    private readonly ILogger<UploadModel> _logger;
    public UploadModel(
        IWeatherService weatherService,
        ILogger<UploadModel> logger)
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
        var uploadResults = new List<ArchiveUploadResultViewModel>();
        foreach (var uploadedFile in BindingEntity.Files!)
        {
            _logger.LogInformation("Saving archive file at the server {fileName}.", uploadedFile.FileName);
            string filePathOnTheServer = Path.Combine(archivesStoragePath, $"archive - {Guid.NewGuid()}.xslx");
            var stream = new FileStream(filePathOnTheServer, FileMode.Create);
            await uploadedFile.CopyToAsync(stream);
            stream.Dispose();

            _logger.LogInformation("Parsing xlsx table file starting.");
            var result = await _weatherService.SaveFromTableAsync(filePathOnTheServer);
            uploadResults.Add(new ArchiveUploadResultViewModel(uploadedFile.FileName, result.IsSuccess));
            System.IO.File.Delete(filePathOnTheServer);
        }

        BindingEntity.UploadResults = uploadResults;
        return Page();
    }
    public class BindingModel
    {
        [Required(ErrorMessage = "Необходимо выбрать хотя бы один файл.")]
        [AllowedFilesExtensions(AllowedExtensions = new string[] { ".xlsx" }, ErrorMessage = "Возможна загрузка только .xlsx файлов.")]
        [MaximumFilesLength(2097152, ErrorMessage = "Максимально разрешенный размер выгружаемых файла\\ов 2 мегабайта.")]                     // 2 mb
        public IEnumerable<IFormFile>? Files { get; set; }

        public IEnumerable<ArchiveUploadResultViewModel>? UploadResults { get; set; }
    }
}
public record ArchiveUploadResultViewModel(string FileName, bool SuccessfullyParsed);