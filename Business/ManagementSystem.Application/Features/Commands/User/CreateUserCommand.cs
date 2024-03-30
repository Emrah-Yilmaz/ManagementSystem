using ManagementSystem.Domain.Models.Args.User;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.User
{
    public class CreateUserCommand : CreateUserArgs, IRequest<int>
    {
    }
}
