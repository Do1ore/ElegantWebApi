using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure.Contracts;
using FluentValidation;
using MediatR;

namespace ElegantWebApi.Application.Features.DeleteDataList
{
    public sealed class DeleteRecordListHandler : IRequestHandler<DeleteRecordListCommand, DataListModel>
    {
        private readonly IConcurrentDictionaryService _dictionaryService;
        private readonly IValidator<DeleteRecordListCommand> _validator;
        public DeleteRecordListHandler(
            IConcurrentDictionaryService dictionaryService,
            IValidator<DeleteRecordListCommand> validator)
        {
            _dictionaryService = dictionaryService;
            _validator = validator;
        }

        public async Task<DataListModel> Handle(DeleteRecordListCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var result = await _dictionaryService.DeleteAsync(request.Id);
            if (result == null)
            {
                throw new ArgumentNullException(nameof(DataListModel));
            }
            return new DataListModel() { Id = Guid.Parse(request.Id), Values = result };
        }
    }
}
