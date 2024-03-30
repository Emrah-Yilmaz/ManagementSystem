using FluentValidation;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetDepartmentQueryValidator : AbstractValidator<GetDepartmentQuery>
    {
        public GetDepartmentQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}
