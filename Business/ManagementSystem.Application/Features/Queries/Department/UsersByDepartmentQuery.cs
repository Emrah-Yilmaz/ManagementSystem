using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class UsersByDepartmentQuery : GetDepartmentArgs, IRequest<UsersByDepartmentDto>
    {
    }
}
