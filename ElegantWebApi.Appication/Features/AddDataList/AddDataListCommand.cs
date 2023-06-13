
using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public class AddDataListCommand : IRequest<DataListModel>
    {
        public DataListModel? listModel { get; }

        public AddDataListCommand(DataListModel? listModel)
        {
            this.listModel = listModel;
        }
    }
}
