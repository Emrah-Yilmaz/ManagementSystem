using AutoMapper;
using ManagementSystem.Application.Features.Commands.City;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public LocationController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpPost("city")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCitiesAsync([FromBody]SyncLocationCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
