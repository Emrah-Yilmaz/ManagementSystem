using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
    }
}
