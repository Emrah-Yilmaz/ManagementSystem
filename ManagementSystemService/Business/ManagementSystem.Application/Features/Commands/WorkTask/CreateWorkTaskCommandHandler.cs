using ManagementSystem.Domain.Services.Abstract.WorkTask;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.WorkTask
{
    public class CreateWorkTaskCommandHandler : IRequestHandler<CreateWorkTaskCommand, int>
    {
        private readonly IWorkTaskService _workTaskService;

        public CreateWorkTaskCommandHandler(IWorkTaskService workTaskService)
        {
            _workTaskService = workTaskService;
        }

        public async Task<int> Handle(CreateWorkTaskCommand request, CancellationToken cancellationToken)
        {
            var result = await _workTaskService.CreateAsync(request, cancellationToken);
            return result;
        }
    }
}
