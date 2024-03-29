using AutoMapper;
using ManagementSystem.Application.Features.Queries.WorkTask;
using ManagementSystem.WebApi.Models.WorkTask.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public WorkTaskController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<WorkTasksResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> All([FromQuery] GetWorkTasksQuery request, CancellationToken cancellationToken = default)
        {
            var query = new GetWorkTasksQuery
            {
                Id = request.Id
            };
            var result = await _mediator.Send(query, cancellationToken);
            var mappedResult = _mapper.Map<List<WorkTasksResponse>>(result);
            return Ok(mappedResult);
        }
    }
}
