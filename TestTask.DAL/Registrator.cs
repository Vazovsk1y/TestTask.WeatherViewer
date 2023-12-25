using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TestTask.DAL;

public static class Registrator
{
	public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration) => services
		.AddDbContext<TestTaskDbContext>(e => e.UseSqlServer(configuration.GetConnectionString("Default")));
}
