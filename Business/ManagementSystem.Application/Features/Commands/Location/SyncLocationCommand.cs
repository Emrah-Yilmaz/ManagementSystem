using ManagementSystem.Domain.Models.Args.LocationArgs;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.City
{
    public class SyncLocationCommand : SyncLocationArgs, IRequest<bool>
    {
    }
}
