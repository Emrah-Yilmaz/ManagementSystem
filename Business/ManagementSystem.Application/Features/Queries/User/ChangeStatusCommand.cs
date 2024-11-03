using ManagementSystem.Domain.Models.Args.User;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class ChangeStatusCommand : ChangeStatusArgs, IRequest<bool>
    {

    }
}
