using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Bogus
{
    public class CreateEntityWithBogusCommandHandler : IRequestHandler<CreateEntityWithBogusCommand, bool>
    {
        private readonly IUserService _userService;

        public CreateEntityWithBogusCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(CreateEntityWithBogusCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateUsersWithBogus(cancellationToken);
            return result;
        }
    }
}
