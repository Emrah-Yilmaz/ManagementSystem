using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Department;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class UsersByDepartmentQueryHandler : IRequestHandler<UsersByDepartmentQuery, UsersByDepartmentDto>
    {
        private readonly IDepartmentService _departmentService;

        public UsersByDepartmentQueryHandler(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<UsersByDepartmentDto> Handle(UsersByDepartmentQuery request, CancellationToken cancellationToken)
        {
            var result = await _departmentService.GetUsersByDepartment(request, cancellationToken);
            return result;
        }
    }
}