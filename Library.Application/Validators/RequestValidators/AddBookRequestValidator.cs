using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Validators.DtoValidators;

namespace Library.Application.Validators.RequestValidators
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(r => r.BookDto)
                .NotNull().WithMessage("Book is required")
                .SetValidator(new BookDtoValidator());
        }
    }
}
