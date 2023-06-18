using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.GetDataList
{
    public record struct GetDataListCommand(string Id) : IRequest<List<object>>
    {
    }
}
