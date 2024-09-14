using FluentValidation;

namespace ManagementSystem.Application.Features.Queries.WorkTask
{
    public  class GetWorkTasksQueryValidator : AbstractValidator<GetWorkTasksQuery>
    {
        public GetWorkTasksQueryValidator()
        {
            RuleFor(p => p.Title).MaximumLength(200);
            RuleFor(p => p.Department).MaximumLength(200);
            RuleFor(p => p.AssignedUser).MaximumLength(200);
            RuleFor(p => p.Status).MaximumLength(200);
            RuleFor(p => p.CreatedBy).MaximumLength(200);
            RuleFor(p => p.ModifiedBy).MaximumLength(200);
        }
    }
}