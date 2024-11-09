using ManagementSystem.Domain.Models.Args.User;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUsersQuery : GetUserArgs, IRequest<List<UserDto>>
    {
    }
}
