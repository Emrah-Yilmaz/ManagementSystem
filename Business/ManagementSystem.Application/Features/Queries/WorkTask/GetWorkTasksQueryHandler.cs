using ManagementSystem.Domain.Services.Abstract;
using ManagementSystem.WebApi.Models.WorkTask.Response;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.WorkTask
{
    public class GetWorkTasksQueryHandler : IRequestHandler<GetWorkTasksQuery, IList<WorkTasksDto>?>
    {
        private readonly IWorkTaskService workTaskService;

        public GetWorkTasksQueryHandler(IWorkTaskService workTaskService)
        {
            this.workTaskService = workTaskService;
        }

        public async Task<IList<WorkTasksDto>?> Handle(GetWorkTasksQuery request, CancellationToken cancellationToken)
        {
            var result = await workTaskService.GetAllAsync(cancellationToken);
            return result;
        }
    }
}
