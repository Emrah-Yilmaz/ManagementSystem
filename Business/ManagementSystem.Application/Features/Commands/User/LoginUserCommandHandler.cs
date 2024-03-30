using ManagementSystem.Domain.Extensions;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.User
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginDto>
    {
        private readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginAsync(request, cancellationToken);
            return result;
        }
    }
}
