using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.WorkTask;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.WorkTask
{
    public class GetWorkTasksQueryHandler : IRequestHandler<GetWorkTasksQuery, IList<WorkTasksDto>>
    {
        private readonly IWorkTaskService _workTaskService;

        public GetWorkTasksQueryHandler(IWorkTaskService workTaskService)
        {
            _workTaskService = workTaskService;
        }

        public async Task<IList<WorkTasksDto>> Handle(GetWorkTasksQuery request, CancellationToken cancellationToken)
        {
            var result = await _workTaskService.GetWorkTasksAsync(request, cancellationToken);
            return result;
        }
    }
}
