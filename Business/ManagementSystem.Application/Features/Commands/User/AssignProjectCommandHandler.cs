using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.User
{
    public class AssignProjectCommandHandler : IRequestHandler<AssignProjectCommand, bool>
    {
        private readonly IUserService _userService;

        public AssignProjectCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(AssignProjectCommand request, CancellationToken cancellationToken)
        {
            return await _userService.AssignUserToProjectAsync(request, cancellationToken);
        }
    }
}
