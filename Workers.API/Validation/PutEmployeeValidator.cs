using FluentValidation;
using Workers.Abstractions;
using Workers.API.Models.Input;

namespace Workers.API.Validation;

public class PutEmployeeValidator : AbstractValidator<PutEmployee>
{
    public PutEmployeeValidator(IPositionsService positionsService)
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(64);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(64);
        RuleFor(x => x.SecondName).NotEmpty().MaximumLength(64);
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.PositionsId)
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("Positions id duplications are not allowed")
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