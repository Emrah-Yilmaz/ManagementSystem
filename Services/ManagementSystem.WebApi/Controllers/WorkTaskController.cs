using ManagementSystem.Application.Features.Queries.WorkTask;
using ManagementSystem.WebApi.Models.WorkTask.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<WorkTasksResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> All(CancellationToken cancellationToken = default)
        {
            var query = new GetWorkTasksQuery();
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
