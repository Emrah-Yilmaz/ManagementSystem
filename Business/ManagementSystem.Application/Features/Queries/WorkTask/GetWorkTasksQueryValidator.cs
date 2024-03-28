using FluentValidation;

namespace ManagementSystem.Application.Features.Queries.WorkTask
{
    public  class GetWorkTasksQueryValidator : AbstractValidator<GetWorkTasksQuery>
    {
        public GetWorkTasksQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(5).WithMessage("5N");
        }
    }
}