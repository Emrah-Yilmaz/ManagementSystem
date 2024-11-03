using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, bool>
    {
        private readonly IUserService _userService;

        public ChangeStatusCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<bool> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            return _userService.ChangeStatusAsync(request, cancellationToken);
        }
    }
}
