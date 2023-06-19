using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ElegantWebApi.Application.Features.AppendValue
{
    public class AppendValueHandler : IRequestHandler<AppendValueCommand, SingleDataModel>
    {
        private readonly IConcurrentDictionaryService _dictionaryService;
        private readonly IExpirationDataService _expirationDataService;
        private readonly IValidator<AppendValueCommand> _validator;
        private readonly IConfiguration _configuration;

        public AppendValueHandler(
            IExpirationDataService expirationDataService,
            IConcurrentDictionaryService dictionaryService,
            IValidator<AppendValueCommand> validator, IConfiguration configuration)
        {
            _expirationDataService = expirationDataService;
            _dictionaryService = dictionaryService;
            _validator = validator;
            _configuration = configuration;
        }

        public async Task<SingleDataModel> Handle(AppendValueCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);
            await _dictionaryService
                .AppendAsync(request.DataModel.Id.ToString(), request.DataModel.Value);
            var defaultExpirationTimeInMinutes =
                Convert.ToInt32(_configuration.GetSection("DefaultExpirationTime")["DefaultExpirationTimeInMinutes"]);
            var defaultExpirationDateTime = DateTime.Now.AddMinutes(defaultExpirationTimeInMinutes);
            
            await _expirationDataService
                .UpdateExpirationTimeAsync(request.DataModel.Id.ToString(), defaultExpirationDateTime);

            return request.DataModel;
        }
    }
}