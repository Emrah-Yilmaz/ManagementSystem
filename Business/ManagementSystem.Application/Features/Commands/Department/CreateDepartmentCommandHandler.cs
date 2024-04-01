using ManagementSystem.Domain.Services.Abstract.Department;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Department
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
    {
        private readonly IDepartmentService _service;

        public CreateDepartmentCommandHandler(IDepartmentService service)
        {
            _service = service;
        }

        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(request, cancellationToken);
            return result;
        }
    }
}
