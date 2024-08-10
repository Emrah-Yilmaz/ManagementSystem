using AutoMapper;
using CommonLibrary.Features.Paginations;
using ManagementSystem.Application.Features.Commands.Department;
using ManagementSystem.Application.Features.Queries.Department;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Department.Request;
using ManagementSystem.WebApi.Models.Department.Resposne;
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

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] GetDepartmentQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(PagedViewModel<DepartmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Departments([FromQuery] DepartmentRequest request, CancellationToken cancellationToken = default)
        {
            var query = new GetDeparmentsQuery
            {
                Page = request.Page,
                PageSize = request.PageSize
            };

            var result = await _mediator.Send(query);
            if (result is null || result.Results.Count == 0)
            {
                return NotFound();
            }
            var mappedResponse = _mapper.Map<PagedViewModel<DepartmentResponse>>(result);

            return Ok(mappedResponse);
        }
    }
}
