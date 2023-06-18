
using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public record struct AddDataListCommand(DataListModel ListModel) : IRequest<DataListModel>
    {

    }
}
