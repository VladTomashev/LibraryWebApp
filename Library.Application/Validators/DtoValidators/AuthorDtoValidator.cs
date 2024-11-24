using FluentValidation;
using Library.Application.DTO.Basics;

namespace Library.Application.Validators.DtoValidators
{
    public class AuthorDtoValidator : AbstractValidator<AuthorDto>
    {
        public AuthorDtoValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Author's firstname is required")
                .Length(2, 50).WithMessage("Author's firstname must be between 2 and 50 characters long.")
                .Matches(@"^[A-Za-zА-Яа-я]*$").WithMessage("Author's firstname must contain only letters");

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Author's lastname is required")
                .Length(2, 50).WithMessage("Author's lastname must be between 2 and 50 characters long.")
                .Matches(@"^[A-Za-zА-Яа-я]*$").WithMessage("Author's lastname must contain only letters");

            RuleFor(r => r.Country)
                .NotEmpty().WithMessage("Author's country is required")
                .Length(3, 50).WithMessage("Author's country must be between 3 and 50 characters long.")
                .Matches(@"^[A-Za-zА-Яа-я\s]*$").WithMessage("Author's country must contain only letters and spaces");

            RuleFor(r => r.DateOfBirth)
                .NotEmpty().WithMessage("Author's date of birth is required.")
                .LessThan(DateTime.Now).WithMessage("Author's date of birth cannot be in the future.");
        }
    }
}
