using Microsoft.EntityFrameworkCore;
using TestTask.DAL.PostgreSQL;

namespace TestTask.Web.Extensions;

public static class Common
{
	public static void MigrateDatabase(this WebApplication application)
	{
		using var scope = application.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();
		dbContext.Database.Migrate();
	}
}