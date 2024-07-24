using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;

namespace TestTask.Web.Pages.Weather.Records;

public class View(IWeatherService weatherService) : PageModel
{
    [BindProperty]
    public Binding BindingModel { get; set; } = new();
    public WeatherRecordsPage? CurrentPage { get; set; }
    
    public static readonly IEnumerable<SelectListItem> Years = Enumerable.Range(2000, DateTime.Now.Year - 2000).Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToString() });
    
    public async Task OnGetAsync(Guid weatherArchiveId, int? byYear = null, Months byMonth = Months.None, int pageIndex = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        BindingModel.SelectedMonth = byMonth; 
        BindingModel.SelectedYear = byYear;
        
        var result = await weatherService.GetRecordsPageAsync(weatherArchiveId, new PagingOptions(pageIndex, pageSize), new WeatherRecordsFilteringOptions(byMonth, byYear), cancellationToken);
        if (result.IsSuccess)
        {
            CurrentPage = result.Value;
        }
    }
    
    public class Binding
    {
        public Months SelectedMonth { get; set; }

        public int? SelectedYear { get; set; }
    }
}