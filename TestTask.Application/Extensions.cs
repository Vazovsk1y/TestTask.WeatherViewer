using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Interfaces;
using TestTask.DAL.Models;

namespace TestTask.Application;

public static class Extensions
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services) => services
		.AddTransient<ITableProvider<WeatherRecord>, XlsxWeatherTableProvider>()
		.AddScoped<IWeatherService, WeatherService>();
}
