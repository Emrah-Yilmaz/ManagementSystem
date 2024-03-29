using ManagementSystem.Domain.Models.Args;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.WorkTask
{
    public class GetWorkTasksQuery : WorkTaskArgs, IRequest<IList<WorkTasksDto>>
    {

    }
}
