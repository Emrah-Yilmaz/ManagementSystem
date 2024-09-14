using ManagementSystem.Domain.Models.Args.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ManagementSystem.Application.Features.Commands.User
{
    [AllowAnonymous]
    public class CreateUserCommand : CreateUserArgs, IRequest<int>
    {
    }
}
