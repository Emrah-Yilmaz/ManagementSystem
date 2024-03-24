using ManagementSystem.WebApi.Models.WorkTask.Response;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.WorkTask
{
    public class GetWorkTasksQuery : IRequest<IList<WorkTasksDto>?>
    {
    }
}
