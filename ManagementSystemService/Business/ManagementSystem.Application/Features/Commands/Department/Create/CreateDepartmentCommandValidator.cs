using FluentValidation;

namespace ManagementSystem.Application.Features.Commands.Department.Create
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().MaximumLength(200);
        }
    }
}
