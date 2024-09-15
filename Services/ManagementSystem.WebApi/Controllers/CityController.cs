using AutoMapper;
using ManagementSystem.Application.Features.Commands.City;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public CityController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpPost()]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken = default)
        {
            var command = new CreateCityCommand();
            var result = await _mediator.Send(command, cancellationToken);

            if (result <= 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
