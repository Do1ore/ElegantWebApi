using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public class AddDataListCommandValidator : AbstractValidator<AddDataListCommand>
    {
        private readonly IConfiguration _configuration;
        public AddDataListCommandValidator(IConfiguration configuration)
        {
            RuleFor(x => x.ListModel).NotNull().NotEmpty();
            _configuration = configuration;
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<AddDataListCommand> context, CancellationToken cancellation = default)
        {
            var result = base.ValidateAsync(context, cancellation);
            if (context.InstanceToValidate.ListModel != null)
            {
                var myDateTime = context.InstanceToValidate.ListModel.ExpirationTime;

                if (myDateTime == default | myDateTime < DateTime.Now)
                {
                    var defaultDateTime = _configuration.GetSection("DefaultExpirationTime")["DefaultExpirationTimeInMinutes"];

                    context.InstanceToValidate.ListModel.ExpirationTime = DateTime.Now.AddMinutes(int.Parse(defaultDateTime!));
                }

            }

            return base.ValidateAsync(context, cancellation);
        }
    }
}
