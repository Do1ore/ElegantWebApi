
using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public class AddDataListCommand : IRequest<DataListModel>
    {
        public DataListModel? ListModel { get; }

        public AddDataListCommand(DataListModel? listModel)
        {
            this.ListModel = listModel;
        }
    }
}
