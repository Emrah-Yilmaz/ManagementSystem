using AutoMapper;
using ManagementSystem.Application.Features.Commands.User;
using ManagementSystem.Application.Features.Queries.User;
using ManagementSystem.WebApi.Models.Response.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers.Admin
{
    [Route("api/admin/user")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AdminUserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet()]
        [ProducesResponseType(typeof(List<UsersResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken = default)
        {
            var query = new GetUsersQuery();
            var result = await _mediator.Send(query, cancellationToken);

            if (result is null || result.Count == 0)
            {
                return NotFound();
            }
            var mappedResponse = _mapper.Map<List<UsersResponse>>(result);
            return Ok(mappedResponse);
        }

        [HttpPost("AddUserToDepartment")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> AddUserToDepartment(AddUserToDepartmentCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result)
                return BadRequest();

            return Ok(result);
        }
    }
}
