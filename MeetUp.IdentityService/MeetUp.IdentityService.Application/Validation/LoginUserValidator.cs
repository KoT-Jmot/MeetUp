using FluentValidation;
using MeetUp.IdentityService.Application.DTOs.InputDto;

namespace MeetUp.IdentityService.Application.Validation
{
    public class LoginUserValidator : AbstractValidator<UserForLoginDto>
    {
        public LoginUserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Enter correct email");

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .Matches("[a-z]+")
                .Matches("[0-9]+")
                .MinimumLength(8)
                .MaximumLength(50)
                .WithMessage("Enter correct password");
        }
    }
}
