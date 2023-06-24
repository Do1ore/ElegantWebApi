using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.GetDataList
{
    public record GetDataListCommand(string Id) : IRequest<List<object>>
    {
    }
}
