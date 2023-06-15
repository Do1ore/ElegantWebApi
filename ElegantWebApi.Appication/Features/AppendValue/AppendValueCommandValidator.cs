using ElegantWebApi.Application.Features.UpdateDataList;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;

namespace ElegantWebApi.Application.Features.AppendValue
{
    public class AppendValueCommandValidator : AbstractValidator<AppendValueCommand>
    {
        public AppendValueCommandValidator(IConfiguration configuration)
        {
            RuleFor(x => x.DataModel).NotNull().NotEmpty();
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<AppendValueCommand> context, CancellationToken cancellation = default)
        {
            if (context.InstanceToValidate.DataModel == null)
            {
                context.AddFailure("ListModel", "Null reference.");
            }
            else if (context.InstanceToValidate.DataModel?.Id == null)
            {
                context.AddFailure("Id", "Null reference.");
            }
            return base.ValidateAsync(context, cancellation);
        }
    }
}
