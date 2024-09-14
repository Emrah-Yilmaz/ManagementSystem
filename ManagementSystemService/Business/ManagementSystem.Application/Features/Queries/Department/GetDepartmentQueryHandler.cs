using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Department;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, DepartmentDto>
    {
        private readonly IDepartmentService _service;

        public GetDepartmentQueryHandler(IDepartmentService service)
        {
            _service = service;
        }

        public async Task<DepartmentDto> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetDepartment(request, cancellationToken);
            return result;
        }
    }
}
