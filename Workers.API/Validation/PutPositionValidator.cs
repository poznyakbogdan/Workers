using FluentValidation;
using Workers.API.Models.Input;

namespace Workers.API.Validation;

public class PutPositionValidator : AbstractValidator<PutPosition>
{
    public PutPositionValidator()
    {
        RuleFor(x => x.Grade).InclusiveBetween(1, 16);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(256);
    }
}