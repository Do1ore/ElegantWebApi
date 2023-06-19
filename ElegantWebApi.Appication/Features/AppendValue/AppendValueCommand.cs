using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.AppendValue
{
    public record AppendValueCommand(SingleDataModel DataModel) : IRequest<SingleDataModel> { }
}
