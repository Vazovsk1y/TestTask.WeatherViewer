using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestTask.Web.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
	private readonly ILogger<IndexModel> _logger = logger;
}