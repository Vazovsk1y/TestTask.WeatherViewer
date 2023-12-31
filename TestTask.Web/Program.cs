using TestTask.Application;
using TestTask.DAL;


namespace TestTask.Web;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddRazorPages();
		builder.Services.AddApplicationLayer();
		builder.Services.AddDAL(builder.Configuration);

		var app = builder.Build();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseAuthorization();
		app.MapRazorPages();

		if (app.Environment.IsDevelopment())
		{
			app.MigrateDatabase();
		}

		app.Run();
	}
}