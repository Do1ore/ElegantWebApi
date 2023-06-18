using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure.Contracts;
using FluentValidation;
using MediatR;

namespace ElegantWebApi.Application.Features.UpdateDataList
{
    public class AppendValueHandler : IRequestHandler<AppendValueCommand, SingleDataModel>
    {
        private readonly IConcurrentDictionaryService _dictionaryService;
        private readonly IExpirationDataService _expirationDataService;
        private readonly IValidator<AppendValueCommand> _validator;

        public AppendValueHandler(
            IExpirationDataService expirationDataService,
            IConcurrentDictionaryService dictionaryService,
            IValidator<AppendValueCommand> validator)
        {
            _expirationDataService = expirationDataService;
            _dictionaryService = dictionaryService;
            _validator = validator;
        }

        public async Task<SingleDataModel> Handle(AppendValueCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);
            await _dictionaryService
                .AppendAsync(request.DataModel.Id.ToString(), request.DataModel.Value);
            await _expirationDataService
                .UpdateExpirationTimeAsync(request.DataModel.Id.ToString(), request.DataModel.ExpirationTime);

            return request.DataModel;
        }
    }
}
