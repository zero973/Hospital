using FluentValidation;
using Hospital.Core.Commands.ResourcesSpent;

namespace Hospital.Core.Validators.Requests.ResourcesSpent;

internal class AddResourceSpentRequestValidator : AbstractValidator<AddResourceSpentRequest>
{
    public AddResourceSpentRequestValidator()
    {
        RuleFor(x => x.Resource)
            .NotNull()
            .NotEmpty()
            .WithMessage("Название затраченного ресурса не должно быть пустым");

        RuleFor(x => x.Comment)
            .NotNull()
            .NotEmpty()
            .WithMessage("Комментарий не должен быть пустым");
    }
}