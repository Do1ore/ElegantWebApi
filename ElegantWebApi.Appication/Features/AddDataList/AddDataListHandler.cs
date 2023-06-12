
using ElegantWebApi.Domain.Entities;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public class AddDataListHandler : IRequestHandler<AddDataListCommand, DataListModel>
    {
        public Task<DataListModel> Handle(AddDataListCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request._listModel!);
        }
    }
}
