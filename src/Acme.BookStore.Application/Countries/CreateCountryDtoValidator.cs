using Acme.BookStore.Countries;
using FluentValidation;

namespace Acme.BookStore.Countries;

/// <summary>
/// FluentValidation validator for CreateCountryDto
/// </summary>
public class CreateCountryDtoValidator : AbstractValidator<CreateCountryDto>
{
    public CreateCountryDtoValidator()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage("Country:NameArRequired")
            .MaximumLength(128)
            .WithMessage("Country:NameArMaxLength");

        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage("Country:NameEnRequired")
            .MaximumLength(128)
            .WithMessage("Country:NameEnMaxLength");
    }
}

