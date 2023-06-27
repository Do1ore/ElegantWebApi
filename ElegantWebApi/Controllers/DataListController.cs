using ElegantWebApi.Application.Features.AddDataList;
using ElegantWebApi.Application.Features.AppendValue;
using ElegantWebApi.Application.Features.DeleteDataList;
using ElegantWebApi.Application.Features.GetDataList;
using ElegantWebApi.Domain.Entities;
using ElegantWebApi.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElegantWebApi.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataListModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AppendValues([FromBody] SingleDataModel value)
        {
            var result = await _mediator.Send(new AppendValueCommand(value));
            return Ok(result);
        }

        /// <summary> 
        /// Remove a dictionary by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataListModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteKeyValuePairAsync([AsParameters] string id)
        {
            var result = await _mediator.Send(new DeleteRecordListCommand(id));
            return Ok(result);
        }
    }
}