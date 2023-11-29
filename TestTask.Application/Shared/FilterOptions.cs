using System.ComponentModel.DataAnnotations;

namespace TestTask.Application.Shared;

public record FilterOptions(Months ByMonth, int? ByYear);

public enum Months
{
	None = 0,
	[Display(Name = "Январь")]
	January = 1,
	[Display(Name = "Февраль")]
	February = 2,
	[Display(Name = "Март")]
	March = 3,
	[Display(Name = "Апрель")]
	April = 4,
	[Display(Name = "Май")]
	May = 5,
	[Display(Name = "Июнь")]
	June = 6,
	[Display(Name = "Июль")]
	July = 7,
	[Display(Name = "Август")]
	August = 8,
	[Display(Name = "Сентябрь")]
	September = 9,
	[Display(Name = "Октябрь")]
	October = 10,
	[Display(Name = "Ноябрь")]
	November = 11,
	[Display(Name = "Декабрь")]
	December = 12
}