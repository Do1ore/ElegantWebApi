using ElegantWebApi.Application.Features.AddDataList;
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
        public IActionResult GetAllValues()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddKeyValuePair([FromBody] DataListModel value)
        {
            var result = await _mediator.Send(new AddDataListCommand(value));
            return Ok(result);

        }

        [HttpPut]
        public IActionResult UpdateKeyValuePair()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteKeyValuePair()
        {
            return Ok();
        }
    }
}
