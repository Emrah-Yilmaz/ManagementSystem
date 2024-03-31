using FluentValidation;

namespace ManagementSystem.Application.Features.Commands.WorkTask
{
    public class CreateWorkTaskCommandValidator: AbstractValidator<CreateWorkTaskCommand>
    {
        public CreateWorkTaskCommandValidator()
        {
            RuleFor(p => p.Title).NotNull();
            RuleFor(p => p.Description).NotNull();
            RuleFor(p => p.StatusId).NotNull().GreaterThan(0);
            RuleFor(p => p.AssignedUserId).NotNull().GreaterThan(0);
            RuleFor(p => p.DepartmentId).NotNull().GreaterThan(0);
            RuleFor(p => p.Deadline).NotNull().GreaterThan(DateTime.Now);
        }
    }
}
