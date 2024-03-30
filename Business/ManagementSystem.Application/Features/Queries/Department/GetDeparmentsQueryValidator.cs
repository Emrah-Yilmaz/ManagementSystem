using FluentValidation;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetDeparmentsQueryValidator : AbstractValidator<GetDeparmentsQuery>
    {
        public GetDeparmentsQueryValidator()
        {
            RuleFor(p => p.Name).MaximumLength(200);
            RuleFor(p => p.CreatedBy).MaximumLength(200);
            RuleFor(p => p.ModifiedBy).MaximumLength(200);
        }
    }
}
