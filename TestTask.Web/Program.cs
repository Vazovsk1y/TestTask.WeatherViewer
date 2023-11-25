using Microsoft.EntityFrameworkCore;
using TestTask.Application;
using TestTask.DAL;


namespace TestTask.Web
{
	public class Program
	{
		public static async Task Main(string[] args)
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

			using var scope = app.Services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<TestTaskSecondContext>();
			await context.Database.MigrateAsync();

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();
			app.MapRazorPages();
			app.Run();
		}
	}
}