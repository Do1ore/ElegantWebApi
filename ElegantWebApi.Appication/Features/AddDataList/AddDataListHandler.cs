
using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public class AddDataListHandler : IRequestHandler<AddDataListCommand, DataListModel>
    {
        public Task<DataListModel> Handle(AddDataListCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DataListModel());
        }
    }
}
