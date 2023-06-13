
using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure;
using FluentValidation;
using MediatR;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public class AddDataListHandler : IRequestHandler<AddDataListCommand, DataListModel>
    {

        private readonly IValidator<AddDataListCommand> _validator;
        private readonly IConcurrentDictionaryHostedService _dictionaryService;
        public AddDataListHandler(IValidator<AddDataListCommand> validator, IConcurrentDictionaryHostedService dictionaryService)
        {
            _validator = validator;
            _dictionaryService = dictionaryService;
        }

        public async Task<DataListModel> Handle(AddDataListCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);


            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            _ = _dictionaryService.Create(request.listModel!.Id.ToString(), request.listModel.Values!);

            return request.listModel!;
        }
    }
}
