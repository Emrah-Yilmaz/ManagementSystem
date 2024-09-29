using ManagementSystem.Application.Features.Commands.Bogus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers.Bogus
{
    [Route("api/[controller]")]
    [ApiController]
    public class BogusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BogusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateBogusData")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateBogusData(CreateEntityWithBogusCommand command, CancellationToken cancellationToken = default)
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
