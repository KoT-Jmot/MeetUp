using FluentValidation;
using MeetUp.CommentsService.Application.DTOs.InputDto;

namespace MeetUp.CommentsService.Application.Validation
{
    public class CommentValidator : AbstractValidator<CommentDto>
    {
        public CommentValidator()
        {
            RuleFor(c => c.Text)
               .NotEmpty()
               .NotNull()
               .MaximumLength(300)
               .WithMessage("Incorrect comment text!");

            RuleFor(c => c.EventId)
               .NotEmpty()
               .NotNull()
               .WithMessage("Incorrect event data!");
        }
    }
}
