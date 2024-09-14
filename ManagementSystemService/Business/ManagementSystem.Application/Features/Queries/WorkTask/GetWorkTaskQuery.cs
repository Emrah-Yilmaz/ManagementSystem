using ManagementSystem.Domain.Models.Args.WorkTask;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.WorkTask
{
    public class GetWorkTaskQuery : GetWorkTaskArgs, IRequest<WorkTasksDto>
    {
    }
}
