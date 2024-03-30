using ManagementSystem.Domain.Models.Args.User;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.User
{
    public class LoginUserCommand : LoginArgs, IRequest<LoginDto>
    {
    }
}
