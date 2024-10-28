using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }
    }
}
