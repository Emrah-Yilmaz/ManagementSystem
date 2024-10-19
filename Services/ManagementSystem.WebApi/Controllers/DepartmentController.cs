using AutoMapper;
using CommonLibrary.Features.Paginations;
using ManagementSystem.Application.Features.Commands.Department.Create;
using ManagementSystem.Application.Features.Commands.Department.Delete;
using ManagementSystem.Application.Features.Commands.Department.Update;
using ManagementSystem.Application.Features.Queries.Department;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Request.Department;
using ManagementSystem.WebApi.Models.Response.Department;
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
        public async Task<IActionResult> Craate([FromBody] CreateDepartmentCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut()]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateDepartmentCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken = default)
        {
            var command = new DeleteDepartmentCommand
            {
                Id = Id
            };
            var result = await _mediator.Send(command);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(DepartmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartment([FromRoute] GetDepartmentQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result is null)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<DepartmentResponse>(result);

            return Ok(mappedResponse);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(PagedViewModel<DepartmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartments([FromQuery] DepartmentRequest request, CancellationToken cancellationToken = default)
        {
            var query = new GetDeparmentsQuery
            {
                Page = request.Page,
                PageSize = request.PageSize,
                Name = request.Name
            };

            var result = await _mediator.Send(query);
            if (result is null || result.Results.Count == 0)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<PagedViewModel<DepartmentResponse>>(result);

            return Ok(mappedResponse);
        }

        [HttpGet("usersByDepartment")]
        [ProducesResponseType(typeof(UsesrByDepartmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersByDepartment([FromQuery] UsersByDepartmentQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result is null)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<UsesrByDepartmentResponse>(result);

            return Ok(mappedResponse);
        }
    }
}
