using FluentValidation;

namespace ManagementSystem.Application.Features.Queries.Comment
{
    public class GetCommentQueryValidator : AbstractValidator<GetCommentQuery>
    {
        public GetCommentQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}