using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.DeleteDataList
{
    public record DeleteRecordListCommand(string Id) : IRequest<DataListModel>
    {
    }
}
