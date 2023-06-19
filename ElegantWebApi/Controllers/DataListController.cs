using ElegantWebApi.Application.Features.AddDataList;
using ElegantWebApi.Application.Features.AppendValue;
using ElegantWebApi.Application.Features.DeleteDataList;
using ElegantWebApi.Application.Features.GetDataList;
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

        /// <summary>
        /// Returns values from dictionary by id. Updates expiration time
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDataListFromIdAsync([AsParameters] string id)
        {
            var result = await _mediator.Send(new GetDataListCommand(id));
            return Ok(result);
        }

        /// <summary>
        /// Creates a new dictionary with list of objects as values.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddKeyValuePair([FromBody] DataListModel value)
        {
            var result = await _mediator.Send(new AddDataListCommand(value));
            return Ok(result);
        }

        /// <summary>
        /// Add values to dictionary by id. If dictionary not exists, it will created
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> AppendValues([FromBody] SingleDataModel value)
        {
            await _mediator.Send(new AppendValueCommand(value));
            return Ok(value);
        }
        
        /// <summary> 
        /// Remove a dictionary by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKeyValuePairAsync([AsParameters] string id)
        {
            var result = await _mediator.Send(new DeleteRecordListCommand(id));
            return Ok(result);
        }
    }
}