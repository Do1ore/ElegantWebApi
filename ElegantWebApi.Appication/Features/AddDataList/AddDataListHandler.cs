
using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure;
using ElegantWebApi.Infrastructure.Contracts;
using FluentValidation;
using MediatR;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public class AddDataListHandler : IRequestHandler<AddDataListCommand, DataListModel>
    {

        private readonly IValidator<AddDataListCommand> _validator;
        private readonly IConcurrentDictionaryService _dictionaryService;
        public AddDataListHandler(IValidator<AddDataListCommand> validator, IConcurrentDictionaryService dictionaryService)
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

            await _dictionaryService.Create(request.listModel!.Id.ToString(), request.listModel.Values!);

            return request.listModel!;
        }
    }
}
