using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;

namespace TestTask.Web.Pages.Weather.Archives;

public class View(IWeatherService weatherService) : PageModel
{
    public Page<WeatherArchiveDTO>? CurrentPage { get; set; }
    
    public async Task OnGetAsync([Range(1, int.MaxValue)]int pageIndex = 1, [Range(1, int.MaxValue)]int pageSize = 10, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return;
        }
        
        var pagingOptions = new PagingOptions(pageIndex, pageSize);
        var result = await weatherService.GetArchivesPageAsync(pagingOptions, cancellationToken);

        if (result.IsSuccess)
        {
            CurrentPage = result.Value;
        }
    }
}