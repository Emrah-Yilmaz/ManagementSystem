using ManagementSystem.Domain.Services.Abstract.Comment;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Comment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, int>
    {
        private readonly ICommentService _commentService;

        public UpdateCommentCommandHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<int> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var result = await _commentService.UpdateAsync(request, cancellationToken);
            return result;
        }
    }
}
