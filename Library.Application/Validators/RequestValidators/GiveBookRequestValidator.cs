using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Validators.DtoValidators;

namespace Library.Application.Validators.RequestValidators
{
    public class GiveBookRequestValidator : AbstractValidator<GiveBookRequest>
    {
        public GiveBookRequestValidator()
        {
            RuleFor(r => r.BookRentalDto)
                .NotNull().WithMessage("Book rental is required")
                .SetValidator(new BookRentalDtoValidator());
        }
    }
}
