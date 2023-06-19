using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public record AddDataListCommand(DataListModel ListModel) : IRequest<DataListModel>
    {

    }
}
