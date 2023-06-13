using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure;
using MediatR;

namespace ElegantWebApi.Application.Features.GetDataList
{
    public class GetDataListHandler : IRequestHandler<GetDataListCommand, DataListModel>
    {
        private readonly IConcurrentDictionaryHostedService _dictionaryService;
        public GetDataListHandler(IConcurrentDictionaryHostedService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public Task<DataListModel> Handle(GetDataListCommand request, CancellationToken cancellationToken)
        {
            var result = _dictionaryService.Get(request.Id).GetAwaiter().GetResult();
            return Task.FromResult(new DataListModel() { Id = Guid.NewGuid(), Values = result });

            return Task.FromResult(new DataListModel());
        }
    }
}
