using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.DAL.PostgreSQL.Infrastructure;

namespace TestTask.DAL.PostgreSQL.Extensions;

public static class Registrator
{
	public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, DatabaseSettings databaseSettings)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(databaseSettings.ConnectionString);
		
		services.AddDbContext<TestTaskDbContext>(e => e.UseNpgsql(databaseSettings.ConnectionString));
		return services;
	}
}
