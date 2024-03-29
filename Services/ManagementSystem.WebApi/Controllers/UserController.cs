using AutoMapper;
using ManagementSystem.Application.Features.Commands.User;
using ManagementSystem.WebApi.Models.User;
using MediatR;
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


        [HttpPost()]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<LoginResponse>(result);
            if (mappedResult is null)
            {
                return BadRequest();
            }

            return Ok(mappedResult);
        }


        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<LoginResponse>(result);
            if (mappedResult is null)
            {
                return NotFound("Kullanıcı Bulunamadı");
            }

            return Ok(mappedResult);
        }
    }
}
