using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Application.Interfaces;
using TestTask.Application.Shared;

namespace TestTask.Web.Pages.Weather;

public class ViewModel : PageModel
{
    private readonly IWeatherService _weatherService;

    [BindProperty]
    public BindingModel BindingEntity { get; set; } = new();

    public ViewModel(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task OnGetAsync()
    {
        var result = await _weatherService.GetAllAsync();
        if (result.IsSuccess)
        {
            BindingEntity.WeatherRecords = result.Value;
        }
    }

    public class BindingModel
    {
        public IEnumerable<WeatherRecordDTO>? WeatherRecords { get; set; }
    }
}
