using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.User
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, int>
    {
        private readonly IUserService _userService;

        public UpdateAddressCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserAddressAsync(request, cancellationToken);
        }
    }
}
