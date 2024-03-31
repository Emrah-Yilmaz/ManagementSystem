using ManagementSystem.Domain.Models.Args.Comment;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Comment
{
    public class CreateCommentCommand : CreateCommentArgs, IRequest<int>
    {
    }
}
