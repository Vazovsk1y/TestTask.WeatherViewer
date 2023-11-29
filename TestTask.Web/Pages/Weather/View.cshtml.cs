using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Application.Interfaces;
using TestTask.Application.Shared;

namespace TestTask.Web.Pages.Weather;

public class ViewModel : PageModel
{
    private readonly IWeatherService _weatherService;
	private readonly int DefaultPageSize;

	[BindProperty]
    public BindingModel BindingEntity { get; set; } = new();

    public ViewModel(
		IWeatherService weatherService, 
		IConfiguration configuration)
    {
		DefaultPageSize = configuration.GetValue<int>(nameof(DefaultPageSize));
		_weatherService = weatherService;
    }

	public async Task OnGetAsync(Months selectedMonth = Months.None, int pageIndex = 1, int? selectedYear = null)
	{
		BindingEntity.SelectedMonth = selectedMonth;
		BindingEntity.SelectedYear = selectedYear;
		var result = await _weatherService.GetAsync(new PagingOptions(pageIndex, DefaultPageSize), new FilterOptions(selectedMonth, selectedYear));
		if (result.IsSuccess)
		{
			BindingEntity.WeatherPage = result.Value;
		}
	}

	public class BindingModel
    {
		public IEnumerable<SelectListItem> Years { get; set; } = Enumerable.Range(2000, DateTime.Now.Year - 2000).Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToString() });

		public WeatherPage? WeatherPage { get; set; }

		public Months SelectedMonth { get; set; }

		public int? SelectedYear { get; set; }
    }
}
