using FluentValidation;

namespace ManagementSystem.Application.Features.Commands.WorkTask
{
    public class UpdateWorkTaskCommandValidator : AbstractValidator<UpdateWorkTaskCommand>
    {
        public UpdateWorkTaskCommandValidator()
        {
            RuleFor(x => x.Title)
           .NotNull()
           .When(x => x.Title != null);

            RuleFor(x => x.Description)
            .NotNull()
            .When(x => x.Description != null);

            RuleFor(x => x.AssignedUserId)
            .GreaterThan(0);

            RuleFor(x => x.DepartmentId)
            .GreaterThan(0);

            RuleFor(x => x.Deadline)
             .NotNull()
             .GreaterThanOrEqualTo(DateTime.Now)
             .When(x => x.Deadline.HasValue);
        }
    }
}