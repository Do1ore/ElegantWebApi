using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.GetDataList
{
    public class GetDataListCommand : IRequest<DataListModel>
    {
        public string Id { get; }

        public GetDataListCommand(string id)
        {
            Id = id;
        }
    }
}
