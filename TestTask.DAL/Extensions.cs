using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TestTask.DAL;

public static class Extensions
{
	public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration) => services
		.AddDbContext<TestTaskSecondContext>(e => e.UseSqlServer(configuration.GetConnectionString("Default")));
}
