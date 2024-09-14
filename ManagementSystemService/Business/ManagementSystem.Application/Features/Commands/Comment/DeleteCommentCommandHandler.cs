using ManagementSystem.Domain.Services.Abstract.Comment;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Comment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly ICommentService _commentService;

        public DeleteCommentCommandHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var result = await _commentService.DeleteAsync(request, cancellationToken);
            return result;
        }
    }
}
