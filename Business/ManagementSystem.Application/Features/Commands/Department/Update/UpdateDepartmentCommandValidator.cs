using FluentValidation;

namespace ManagementSystem.Application.Features.Commands.Department.Update
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
            RuleFor(p => p.Name).NotNull().MaximumLength(200);
        }
    }
}
