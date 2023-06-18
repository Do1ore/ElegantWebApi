using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure.Contracts;
using MediatR;

namespace ElegantWebApi.Application.Features.GetDataList
{
    public class GetDataListHandler : IRequestHandler<GetDataListCommand, List<object>>
    {
        private readonly IConcurrentDictionaryService _dictionaryService;

        public GetDataListHandler(IConcurrentDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public async Task<List<object>> Handle(GetDataListCommand request, CancellationToken cancellationToken)
        {
            var valuesList = await _dictionaryService.GetAsync(request.Id);
            return valuesList;

        }
    }
}
