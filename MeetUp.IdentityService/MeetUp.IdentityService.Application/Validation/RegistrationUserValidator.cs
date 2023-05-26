using FluentValidation;
using MeetUp.IdentityService.Application.DTOs.InputDto;

namespace MeetUp.IdentityService.Application.Validation
{
    public class RegistrationUserValidator : AbstractValidator<UserForRegistrationDto>
    {
        public RegistrationUserValidator()
        {
            RuleFor(u => u.UserName)
               .NotEmpty()
               .NotNull()
               .MaximumLength(30)
               .WithMessage("Enter correct name");

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .Matches("[a-z]+")
                .Matches("[0-9]+")
                .MinimumLength(8)
                .MaximumLength(50)
                .WithMessage("Enter correct password");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .Matches("[+]{1}[0-9]{10,12}")
                .WithMessage("Enter correct phone number");

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Enter correct email");
        }
    }
}
