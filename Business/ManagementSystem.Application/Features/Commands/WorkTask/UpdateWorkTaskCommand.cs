using ManagementSystem.Domain.Models.Args.WorkTask;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.WorkTask
{
    public class UpdateWorkTaskCommand : UpdateWorkTaskArgs,IRequest<int>
    {
    }
}
