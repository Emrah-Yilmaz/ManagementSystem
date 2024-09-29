using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.User
{
    public class AddUserToDepartmentCommandHandler : IRequestHandler<AddUserToDepartmentCommand, bool>
    {
        private readonly IUserService _userService;

        public AddUserToDepartmentCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(AddUserToDepartmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.AddUserToDepartment(request, cancellationToken);
            return result;
        }
    }
}
