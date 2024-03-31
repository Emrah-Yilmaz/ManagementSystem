using FluentValidation;

namespace ManagementSystem.Application.Features.Commands.Comment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(p => p.Content).NotNull().MaximumLength(1000);
        }
    }
}
