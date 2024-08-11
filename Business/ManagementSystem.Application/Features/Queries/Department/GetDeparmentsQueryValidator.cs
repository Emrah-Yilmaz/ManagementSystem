using FluentValidation;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetDeparmentsQueryValidator : AbstractValidator<GetDeparmentsQuery>
    {
        public GetDeparmentsQueryValidator()
        {
            RuleFor(p => p.Name).MaximumLength(3);
        }
    }
}
