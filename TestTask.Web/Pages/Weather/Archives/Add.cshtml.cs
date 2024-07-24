using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;
using TestTask.Web.Validators;

namespace TestTask.Web.Pages.Weather.Archives;

public class Add(IWeatherService weatherService) : PageModel
{
    [BindProperty]
    public Binding BindingModel { get; set; } = new();

    public static readonly IReadOnlyDictionary<string, string> Localities = new Dictionary<string, string>()
    {
        {"Amsterdam", "W. Europe Standard Time"},
        {"Athens", "GTB Standard Time"},
        {"Belgrade", "Central Europe Standard Time"},
        {"Berlin", "W. Europe Standard Time"},
        {"Brussels", "Romance Standard Time"},
        {"Budapest", "Central European Standard Time"},
        {"Copenhagen", "Romance Standard Time"},
        {"Dublin", "GMT Standard Time"},
        {"Helsinki", "FLE Standard Time"},
        {"Kyiv", "FLE Standard Time"},
        {"Lisbon", "GMT Standard Time"},
        {"London", "GMT Standard Time"},
        {"Luxembourg", "W. Europe Standard Time"},
        {"Madrid", "Romance Standard Time"},
        {"Monaco", "Romance Standard Time"},
        {"Moscow", "Russian Standard Time"},
        {"Oslo", "W. Europe Standard Time"},
        {"Paris", "Romance Standard Time"},
        {"Prague", "Central Europe Standard Time"},
        {"Riga", "FLE Standard Time"},
        {"Rome", "W. Europe Standard Time"},
        {"Sofia", "FLE Standard Time"},
        {"Stockholm", "W. Europe Standard Time"},
        {"Tallinn", "FLE Standard Time"},
        {"Vienna", "W. Europe Standard Time"},
        {"Warsaw", "Central European Standard Time"}
    };
    
    public static readonly IEnumerable<SelectListItem> LocalitiesKeys = Localities.Keys.Select(e => new SelectListItem(e, e));
    
    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var uploadedFilePath = Path.Combine(Path.GetTempPath(), $"archive - {Guid.NewGuid()}.xlsx");
        var stream = new FileStream(uploadedFilePath, FileMode.Create);
        await BindingModel.SelectedFile.CopyToAsync(stream, cancellationToken);
        await stream.DisposeAsync();
        
        var dto = new WeatherArchiveAddDTO(BindingModel.Title.Trim(), BindingModel.SelectedLocality, Localities[BindingModel.SelectedLocality], uploadedFilePath);
        var result = await weatherService.AddArchiveAsync(dto, cancellationToken);
        System.IO.File.Delete(uploadedFilePath);

        if (result.IsSuccess)
        {
            return Redirect("/Weather/Archives/View");
        }

        return Page();
    }
    
    public class Binding
    {
        [Required(ErrorMessage = "You must select the file.")]
        [AllowedFileExtensions(AllowedExtensions = [".xlsx"], ErrorMessage = "Only .xlsx files allowed.")]
        [MaximumFileLength(2097152, ErrorMessage = "The maximum allowable size of the uploaded file is 2 megabytes.")]     // 2 mb
        public IFormFile SelectedFile { get; init; } = null!;

        [Required(ErrorMessage = "Title property is required.")]
        public string Title { get; init; } = null!;

        [Required(ErrorMessage = "You must select locality.")] 
        public string SelectedLocality { get; init; } = null!;
    }
}