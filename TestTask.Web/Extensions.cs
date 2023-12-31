using Microsoft.EntityFrameworkCore;
using TestTask.DAL;

namespace TestTask.Web;

public static class Extensions
{
	public static void MigrateDatabase(this WebApplication application)
	{
		using var scope = application.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();
		dbContext.Database.Migrate();
	}
}
