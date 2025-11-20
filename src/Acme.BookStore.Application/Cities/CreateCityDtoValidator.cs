using Acme.BookStore.Cities;
using FluentValidation;

namespace Acme.BookStore.Cities;

/// <summary>
/// FluentValidation validator for CreateCityDto
/// </summary>
public class CreateCityDtoValidator : AbstractValidator<CreateCityDto>
{
    public CreateCityDtoValidator()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage("City:NameArRequired")
            .MaximumLength(128)
            .WithMessage("City:NameArMaxLength");

        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage("City:NameEnRequired")
            .MaximumLength(128)
            .WithMessage("City:NameEnMaxLength");

        RuleFor(x => x.CountryId)
            .NotEmpty()
            .WithMessage("City:CountryIdRequired");
    }
}

