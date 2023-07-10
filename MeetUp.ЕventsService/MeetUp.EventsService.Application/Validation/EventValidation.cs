using FluentValidation;
using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;

namespace MeetUp.EventsService.Application.Validation
{
    public class EventValidation : AbstractValidator<EventDto>
    {
        public EventValidation()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(50)
                .WithMessage("Enter correct Title!");

            RuleFor(p => p.Description)
                .MaximumLength(300)
                .WithMessage("Enter correct Description!");

            RuleFor(p => p.Place)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .WithMessage("Enter correct place of event!");

            RuleFor(p => p.DateStart)
                .NotEmpty()
                .NotNull()
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Enter correct date of event!"); ;

            RuleFor(p => p.CategoryId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Enter correct type of event!");
        }
    }
}
