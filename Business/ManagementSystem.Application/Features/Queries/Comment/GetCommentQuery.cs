using ManagementSystem.Domain.Models.Args.Comment;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Comment
{
    public class GetCommentQuery : GetCommentArgs, IRequest<GetCommentDto>
    {
    }
}
