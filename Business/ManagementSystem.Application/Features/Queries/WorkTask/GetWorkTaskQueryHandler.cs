using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.WorkTask
{
    public class GetWorkTaskQueryHandler : IRequestHandler<GetWorkTaskQuery, WorkTasksDto>
    {
        private readonly IWorkTaskService _workTaskService;

        public GetWorkTaskQueryHandler(IWorkTaskService workTaskService)
        {
            _workTaskService = workTaskService;
        }

        public async Task<WorkTasksDto> Handle(GetWorkTaskQuery request, CancellationToken cancellationToken)
        {
            var result = await _workTaskService.GetWorkTaskAsync(request, cancellationToken);
            return result;
        }
    }
}
