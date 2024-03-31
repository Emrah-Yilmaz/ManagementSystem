using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Args.WorkTask;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.WorkTask
{
    public class CreateWorkTaskCommand : CreateWorkTaskArgs, IRequest<int>
    {
    }
}
