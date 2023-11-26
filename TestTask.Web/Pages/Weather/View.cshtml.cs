using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Application.Interfaces;
using TestTask.Application.Shared;

namespace TestTask.Web.Pages.Weather;

public class ViewModel : PageModel
{
    private readonly IWeatherService _weatherService;
	private readonly int DefaultPageSize;

	[BindProperty]
    public BindingModel BindingEntity { get; set; } = new();

    public ViewModel(IWeatherService weatherService, IConfiguration configuration)
    {
		DefaultPageSize = configuration.GetValue<int>(nameof(DefaultPageSize));
		_weatherService = weatherService;
    }

	public async Task OnGetAsync(int pageIndex = 1)
	{
		var result = await _weatherService.GetAsync(new PagingOptions(pageIndex, DefaultPageSize));
		if (result.IsSuccess)
		{
			BindingEntity.WeatherPage = result.Value;
		}
	}

	public class BindingModel
    {
        public WeatherPage? WeatherPage { get; set; }
    }
}
