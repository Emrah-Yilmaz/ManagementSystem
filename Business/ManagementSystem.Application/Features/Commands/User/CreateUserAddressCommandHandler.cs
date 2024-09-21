using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.User
{
    public class CreateUserAddressCommandHandler : IRequestHandler<CreateUserAddressCommand, bool>
    {
        private readonly IUserService _userService;

        public CreateUserAddressCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateUserAddressAsync(request, cancellationToken);
            return result;
        }
    }
}
