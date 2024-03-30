using AutoMapper;
using ManagementSystem.Application.Features.Commands.Department;
using ManagementSystem.WebApi.Models.Department.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public DepartmentController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CreateDepartmentCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
