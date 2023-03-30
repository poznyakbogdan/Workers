using FluentValidation;
using Workers.API.Models.Input;

namespace Workers.API.Validation;

public class PostPositionValidator : AbstractValidator<PostPosition>
{
    public PostPositionValidator()
    {
        RuleFor(x => x.Grade).InclusiveBetween(1, 16);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(256);
    }
}