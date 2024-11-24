using FluentValidation;
using Library.Application.DTO.Basics;

namespace Library.Application.Validators.DtoValidators
{
    public class BookRentalDtoValidator : AbstractValidator<BookRentalDto>
    {
        public BookRentalDtoValidator()
        {
            RuleFor(r => r.UserProfileId)
                .NotEmpty().WithMessage("User is required");

            RuleFor(r => r.BookId)
                .NotEmpty().WithMessage("Book is required");

            RuleFor(r => r.TakingTime)
                .NotEmpty().WithMessage("Taking time is required");

            RuleFor(r => r.ReturnTime)
                .NotEmpty().WithMessage("Return time is required")
                .GreaterThan(r => r.TakingTime).WithMessage("Return time must be greater than taking time.");
        }
    }
}
