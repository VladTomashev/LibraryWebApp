using FluentValidation;
using Library.Application.DTO.Requests;

namespace Library.Application.Validators.RequestValidators
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(r => r.Login)
                .NotEmpty().WithMessage("Login is required")
                .Length(3, 50).WithMessage("Login length must be from 3 to 50 characters")
                .Matches(@"^[A-Za-z0-9_]*$").WithMessage("Login must contain only letters, numbers or underline");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(8, 50).WithMessage("Password length must be from 8 to 50 characters");

            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Firstname is required")
                .Length(3, 50).WithMessage("Firstname length must be from 3 to 50 characters")
                .Matches(@"^[a-zA-Zа-яА-Я\s]+$").WithMessage("Firstname must contain only letters and spaces");

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Lastname is required")
                .Length(3, 50).WithMessage("Lastname length must be from 3 to 50 characters")
                .Matches(@"^[a-zA-Zа-яА-Я\s]+$").WithMessage("Lastname must contain only letters and spaces");

            RuleFor(r => r.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Incorrect phone number");

        }
    }
}
