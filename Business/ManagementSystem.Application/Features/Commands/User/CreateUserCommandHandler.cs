using ManagementSystem.Domain.Services.Abstract;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateAsync(request, cancellationToken);
            return result;
        }
    }
}
