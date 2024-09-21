using AutoMapper;
using ManagementSystem.Application.Features.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command);

            if (result <= 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("CreateAddress")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAddress([FromBody] CreateUserAddressCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
