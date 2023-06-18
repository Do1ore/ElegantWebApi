
using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure;
using ElegantWebApi.Infrastructure.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public class AddDataListHandler : IRequestHandler<AddDataListCommand, DataListModel>
    {

        private readonly IValidator<AddDataListCommand> _validator;
        private readonly IConcurrentDictionaryService _dictionaryService;
        private readonly IExpirationDataService _expirationService;
        public AddDataListHandler(
            IValidator<AddDataListCommand> validator,
            IConcurrentDictionaryService dictionaryService,
            IExpirationDataService expirationService)
        {
            _validator = validator;
            _dictionaryService = dictionaryService;
            _expirationService = expirationService;
        }

        public async Task<DataListModel> Handle(AddDataListCommand request, CancellationToken cancellationToken)
        {
            _ = await _validator.ValidateAsync(request, cancellationToken);

            await _dictionaryService
                .CreateAsync(request.ListModel!.Id.ToString(), request.ListModel.Values!);

            await _expirationService
                .AddExpirationTimeAsync(request.ListModel!.Id.ToString(), request.ListModel!.ExpirationTime);


            return request.ListModel!;
        }
    }
}
