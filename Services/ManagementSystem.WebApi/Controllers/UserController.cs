﻿using AutoMapper;
using ManagementSystem.Application.Features.Commands.User;
using ManagementSystem.Application.Features.Queries.User;
using ManagementSystem.Domain.TokenHandler;
using ManagementSystem.WebApi.Models.Response.User;
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
        private readonly IDomainPrincipal _domainPrincipal;

        public UserController(IMediator mediator, IMapper mapper, IDomainPrincipal domainPrincipal)
        {
            _mediator = mediator;
            _mapper = mapper;
            _domainPrincipal = domainPrincipal;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAddress([FromBody] CreateUserAddressCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok(result);
        }


        [HttpPut("UpdateAddress")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAddress([FromBody] CreateUserAddressCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser(CancellationToken cancellationToken = default)
        {
            var userId = _domainPrincipal.GetClaims()?.Id;
            if (!userId.HasValue)
                return BadRequest();

            var query = new GetUserQuery();
            query.UserId = userId.Value;
            var result = await _mediator.Send(query);
            var mappedResult = _mapper.Map<UserResponse>(result);
            return Ok(mappedResult);
        }

        [HttpPost("AssignDepartment")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> AssignDepartment(AddUserToDepartmentCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result)
                return BadRequest();

            return Ok(result);
        }
        [HttpPost("AssignProject")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignProject(AssignProjectCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result)
                return BadRequest();

            return Ok(result);
        }
        [HttpGet("UsersWithRelations")]
        [ProducesResponseType(typeof(List<UsersResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersWithRelations(CancellationToken cancellationToken = default)
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
        [HttpPatch("change-status")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeStatus([FromQuery] ChangeStatusCommand request, CancellationToken cancellationToken = default)
        {
            var command = await _mediator.Send(request, cancellationToken);
            if (!command)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
