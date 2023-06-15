using ElegantWebApi.Application.Features.AddDataList;
using ElegantWebApi.Application.Features.DeleteDataList;
using ElegantWebApi.Application.Features.GetDataList;
using ElegantWebApi.Application.Features.UpdateDataList;
using ElegantWebApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElegantWebApi.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DataListController : ControllerBase
    {

        private readonly IMediator _mediator;

        public DataListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetDataListFromIdAsync([AsParameters] string id)
        {
            var result = await _mediator.Send(new GetDataListCommand(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddKeyValuePair([FromBody] DataListModel value)
        {
            var result = await _mediator.Send(new AddDataListCommand(value));
            return Ok(result);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateKeyValuePair([FromBody] SingleDataModel value)
        {
            await _mediator.Send(new AppendValueCommand(value));
            return Ok(value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKeyValuePairAsync([AsParameters] string id)
        {
            var result = await _mediator.Send(new DeleteRecordListCommand(id));
            return Ok(result);
        }
    }
}
