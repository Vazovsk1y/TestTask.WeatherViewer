using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Validators;

public class WeatherArchiveAddDTOValidator : AbstractValidator<WeatherArchiveAddDTO>
{
    public WeatherArchiveAddDTOValidator()
    {
        RuleFor(e => e.LocalityTimeZoneId).NotEmpty();
        RuleFor(e => e.LocalityTitle).NotEmpty();
        RuleFor(e => e.FilePath).NotEmpty().Must(e => Path.GetExtension(e) == ".xlsx");
        RuleFor(e => e.Title).NotEmpty();
    }
}