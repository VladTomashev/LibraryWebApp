using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Validators.DtoValidators;

namespace Library.Application.Validators.RequestValidators
{
    public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(r => r.BookId)
            .NotEmpty().WithMessage("Id is required.");

            RuleFor(r => r.BookDto)
            .NotNull().WithMessage("Book is required")
            .SetValidator(new BookDtoValidator());
        }
    }
}
