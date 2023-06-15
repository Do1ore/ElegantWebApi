using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.UpdateDataList
{
    public record struct AppendValueCommand(SingleDataModel DataModel) : IRequest<SingleDataModel> { }
}
