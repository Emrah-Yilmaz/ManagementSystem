using ManagementSystem.Domain.Services.Abstract.WorkTask;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.WorkTask
{
    public class UpdateWorkTaskCommandHandler : IRequestHandler<UpdateWorkTaskCommand, int>
    {
        private readonly IWorkTaskService _workTaskService;

        public UpdateWorkTaskCommandHandler(IWorkTaskService workTaskService)
        {
            _workTaskService = workTaskService;
        }
        public async Task<int> Handle(UpdateWorkTaskCommand request, CancellationToken cancellationToken)
        {
            var result = await _workTaskService.UpdateAsync(request, cancellationToken);
            return result;
        }
    }
}
