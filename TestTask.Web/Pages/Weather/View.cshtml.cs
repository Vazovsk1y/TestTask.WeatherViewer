using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;

namespace TestTask.Web.Pages.Weather;

public class ViewModel : PageModel
{
    private readonly IWeatherService _weatherService;
	private readonly int DefaultPageSize;

    public static readonly IEnumerable<SelectListItem> Years = Enumerable.Range(2000, DateTime.Now.Year - 2000).Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToString() });

    [BindProperty]
    public Binding BindingModel { get; set; } = new();

    public ViewModel(
		IWeatherService weatherService, 
		IConfiguration configuration)
    {
		DefaultPageSize = configuration.GetValue<int>(nameof(DefaultPageSize));
		_weatherService = weatherService;
    }

	public async Task OnGetAsync(Months selectedMonth = Months.None, int pageIndex = 1, int? selectedYear = null)
	{
		BindingModel.SelectedMonth = selectedMonth;
		BindingModel.SelectedYear = selectedYear;
		var result = await _weatherService.GetAsync(new PagingOptions(pageIndex, DefaultPageSize), new WeatherRecordsFilteringOptions(selectedMonth, selectedYear));
		if (result.IsSuccess)
		{
			BindingModel.Page = result.Value;
		}
	}

    public class Binding
    {
        public WeatherRecordsPage? Page { get; set; }

        public Months SelectedMonth { get; set; }

        public int? SelectedYear { get; set; }
    }
}


