using ManagementSystem.Domain.Services.Abstract.Department;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Department.Update
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, int>
    {
        private readonly IDepartmentService _service;

        public UpdateDepartmentCommandHandler(IDepartmentService service)
        {
            _service = service;
        }

        public async Task<int> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(request, cancellationToken);
            return result;
        }
    }
}
