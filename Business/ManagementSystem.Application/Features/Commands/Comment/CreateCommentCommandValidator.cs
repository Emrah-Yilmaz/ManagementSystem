using FluentValidation;

namespace ManagementSystem.Application.Features.Commands.Comment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(p => p.Content).NotNull().MaximumLength(1000);
            RuleFor(p => p.UserId).GreaterThan(0);
        }
    }
}
