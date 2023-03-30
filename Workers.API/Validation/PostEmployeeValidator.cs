using FluentValidation;
using Workers.Abstractions;
using Workers.API.Models.Input;

namespace Workers.API.Validation;

public class PostEmployeeValidator : AbstractValidator<PostEmployee>
{
    public PostEmployeeValidator(IPositionsService positionsService)
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(64);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(64);
        RuleFor(x => x.SecondName).NotEmpty().MaximumLength(64);
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.PositionsId)
            .Must((_, positionsId, context) =>
            {
                var (ok, notExist) = positionsService.Exists(positionsId);

                if (!ok)
                    context.MessageFormatter.AppendArgument("ids", string.Join(",", notExist));

                return ok;
            })
            .WithMessage("Specified positions id does not exist: {ids}");
    }
}