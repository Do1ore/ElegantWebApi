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
            _configuration = configuration;
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<AddDataListCommand> context, CancellationToken cancellation = default)
        {
            var result = base.ValidateAsync(context, cancellation);
            if (context.InstanceToValidate.ListModel != null)
            {
                var myDateTime = context.InstanceToValidate.ListModel.ExpirationTime;
                var maxExirationTime = Convert.ToInt32(_configuration.GetSection("DefaultExpirationTime")["MaxEpirationTimeInMinutes"]);
                var defaultExpirationTime = Convert.ToInt32(_configuration.GetSection("DefaultExpirationTime")["DefaultExpirationTimeInMinutes"]);

                if (myDateTime == default | myDateTime < DateTime.Now | GetMinutesDifference(myDateTime) > maxExirationTime)
                {
                    context.InstanceToValidate.ListModel.ExpirationTime = DateTime.Now.AddMinutes(defaultExpirationTime);
                }

            }

            return base.ValidateAsync(context, cancellation);
        }

        public int GetMinutesDifference(DateTime givenDateTime)
        {
            DateTime now = DateTime.Now;
            TimeSpan difference = now - givenDateTime;
            return (int)difference.TotalMinutes;
        }
    }
}
