using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure.Contracts;
using MediatR;

namespace ElegantWebApi.Application.Features.GetDataList
{
    public class GetDataListHandler : IRequestHandler<GetDataListCommand, DataListModel>
    {
        private readonly IConcurrentDictionaryService _dictionaryService;

        public GetDataListHandler(IConcurrentDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public async Task<DataListModel> Handle(GetDataListCommand request, CancellationToken cancellationToken)
        {
            var valuesList = await _dictionaryService.Get(request.Id);
            DataListModel model = new DataListModel()
            {
                Id = Guid.Parse(request.Id),
                Values = valuesList
            };
            return model;

        }
    }
}
