using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;

namespace ElegantWebApi.Application.Features.AppendValue
{
    public class AppendValueCommandValidator : AbstractValidator<AppendValueCommand>
    {
        public AppendValueCommandValidator(IConfiguration configuration)
        {
            RuleFor(x => x.DataModel.Id).NotNull().NotEmpty()
                .WithMessage("Value must be not null or empty");
            RuleFor(expression: x => x.DataModel.Value).NotEmpty().NotNull()
                .WithMessage("Value must be not null or empty");
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<AppendValueCommand> context,
            CancellationToken cancellation = default)
        {
            if (context.InstanceToValidate.DataModel?.Id == null)
            {
                context.AddFailure("Id", "Null reference.");
            }

            return base.ValidateAsync(context, cancellation);
        }
    }
}