using AutoMapper;
using ManagementSystem.Application.Features.Commands.Comment;
using ManagementSystem.Application.Features.Queries.Comment;
using ManagementSystem.WebApi.Models.Comment.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CommentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost()]
        public async Task<IActionResult> Create(CreateCommentCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut()]
        public async Task<IActionResult> Update(UpdateCommentCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete()]
        public async Task<IActionResult> Delete(DeleteCommentCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [ProducesResponseType(typeof(GetCommentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetCommentQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);
            var mappedResult = _mapper.Map<GetCommentResponse>(result);
            if (mappedResult is null)
            {
                return NotFound();
            }

            return Ok(mappedResult);
        }
    }
}
