using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Comment;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Comment
{
    public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, GetCommentDto>
    {
        private readonly ICommentService _commentService;

        public GetCommentQueryHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<GetCommentDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            var result = await _commentService.GetAsync(request, cancellationToken);
            return result;
        }
    }
}
