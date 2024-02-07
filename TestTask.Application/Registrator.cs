using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Interfaces;
using TestTask.Application.Services;
using TestTask.Application.Services.Interfaces;
using TestTask.DAL.Models;

namespace TestTask.Application;

public static class Registrator
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services) => services
		.AddTransient<IFileParser<WeatherRecord>, XlsxFileParser>()
		.AddScoped<IWeatherService, WeatherService>();
}