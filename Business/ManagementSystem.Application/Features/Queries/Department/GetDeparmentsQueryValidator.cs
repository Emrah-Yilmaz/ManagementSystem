using FluentValidation;
using Packages.Pipelines.Validation;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetDeparmentsQueryValidator : AbstractValidator<GetDeparmentsQuery>, IRequestValidator
    {
        public GetDeparmentsQueryValidator()
        {
            RuleFor(p => p.Name).MaximumLength(3);
        }
    }
}
