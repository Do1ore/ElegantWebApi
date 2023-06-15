using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.DeleteDataList
{
    public record struct DeleteRecordListCommand(string Id) : IRequest<DataListModel>
    {
    }
}
