using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure.Contracts;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ElegantWebApi.Application.Features.GetDataList
{
    public class GetDataListHandler : IRequestHandler<GetDataListCommand, List<object>>
    {
        private readonly IConcurrentDictionaryService _dictionaryService;
        private readonly IExpirationDataService _expirationDataService;
        private readonly IConfiguration _configuration;

        public GetDataListHandler(
            IConcurrentDictionaryService dictionaryService,
            IExpirationDataService expirationDataService, IConfiguration configuration)
        {
            _dictionaryService = dictionaryService;
            _expirationDataService = expirationDataService;
            _configuration = configuration;
        }

        public async Task<List<object>> Handle(GetDataListCommand request, CancellationToken cancellationToken)
        {
            var valuesList = await _dictionaryService.GetAsync(request.Id);

            var defaultExpirationTimeInMinutes =
                Convert.ToInt32(_configuration.GetSection("DefaultExpirationTime")["DefaultExpirationTimeInMinutes"]);
            var defaultExpirationDateTime = DateTime.Now.AddMinutes(defaultExpirationTimeInMinutes);

            await _expirationDataService.UpdateExpirationTimeAsync(request.Id, defaultExpirationDateTime);
            return valuesList;
        }
    }
}