using FluentValidation;

namespace ManagementSystem.Application.Features.Queries.WorkTask
{
    public class GetWorkTaskQueryValidator : AbstractValidator<GetWorkTaskQuery>
    {
        public GetWorkTaskQueryValidator()
        {
            RuleFor(P => P.Id).GreaterThan(0);
        }
    }
}
