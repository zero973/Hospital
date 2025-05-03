using FluentValidation;
using Hospital.Core.Models.Entities;

namespace Hospital.Core.Validators.Models;

internal class ResourceSpentValidator : AbstractValidator<ResourceSpent>
{
    public ResourceSpentValidator()
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