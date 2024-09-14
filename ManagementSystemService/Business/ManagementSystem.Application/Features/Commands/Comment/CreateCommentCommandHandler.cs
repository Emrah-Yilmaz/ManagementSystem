using ManagementSystem.Domain.Services.Abstract.Comment;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Comment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly ICommentService _commentService;

        public CreateCommentCommandHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var result = await _commentService.CreateAsync(request, cancellationToken);
            return result;
        }
    }
}
