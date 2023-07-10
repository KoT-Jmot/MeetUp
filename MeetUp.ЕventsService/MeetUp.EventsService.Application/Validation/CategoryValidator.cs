using FluentValidation;
using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;

namespace MeetUp.EventsService.Application.Validation
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(20)
                .WithMessage("Incorrect category name!");
        }
    }
}
