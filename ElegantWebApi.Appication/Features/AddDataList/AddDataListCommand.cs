
using ElegantWebApi.Domain.Entities;
using MediatR;

namespace ElegantWebApi.Application.Features.AddDataList
{
    public class AddDataListCommand : IRequest<DataListModel>
    {
        public DataListModel? _listModel { get; }

        public AddDataListCommand(DataListModel? listModel)
        {
            _listModel = listModel;
        }
    }
}
