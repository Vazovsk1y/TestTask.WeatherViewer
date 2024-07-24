using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.Application.Services.Interfaces;

namespace TestTask.Application;

public static class Registrator
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services) => services
		.AddTransient<IFileReader<WeatherRecordAddDTO>, WeatherRecordAddDTOXlsxReader>()
		.AddScoped<IWeatherService, WeatherService>()
		.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}