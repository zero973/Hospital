using FluentValidation;

namespace Hospital.Core.Validators.Fields;

internal class FIOValidator : AbstractValidator<string>
{

    public FIOValidator()
    {
        RuleFor(x => x)
            .Length(1, 255)
            .WithMessage("ФИО должно быть больше 1 символа")
            .Must(BeAValidFIO)
            .WithMessage("ФИО содержит некорректны(й/е) символ(-ы)");
    }

    private bool BeAValidFIO(string number)
    {
        var allowedSymbols = "ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ. ";
        var lowerAllowedSymbols = allowedSymbols.ToLower();
        foreach (var c in number)
            if (!allowedSymbols.Contains(c) && !lowerAllowedSymbols.Contains(c))
                return false;
        return true;
    }

}