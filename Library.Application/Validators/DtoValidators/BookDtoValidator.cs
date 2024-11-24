using FluentValidation;
using Library.Application.DTO.Basics;

namespace Library.Application.Validators.DtoValidators
{
    public class BookDtoValidator : AbstractValidator<BookDto>
    {
        public BookDtoValidator()
        {
            RuleFor(r => r.Isbn)
                .NotEmpty().WithMessage("ISBN is required")
                .Matches(@"^(978|979)-\d{1,5}-\d{1,7}-\d{1,7}-\d{1}$")
                    .WithMessage("ISBN must be a valid ISBN-13 format (e.g., 978-3-16-148410-0)")
                .Matches(@"^\d{9}[\dX]$").When(r => r.Isbn.Length == 10)
                    .WithMessage("ISBN must be a valid ISBN-10 format (e.g., 0-306-40615-2)");

            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Book's name is required")
                .Length(1, 50).WithMessage("Book's name must be between 1 and 50 characters long.")
                .Matches(@"^[A-Za-zА-Яа-я0-9\s]*$").WithMessage("Book's name must contain only letters, numbers or spaces");

            RuleFor(r => r.Genre)
                .NotEmpty().WithMessage("Book's genre is required")
                .Length(1, 50).WithMessage("Book's genre must be between 1 and 50 characters long.")
                .Matches(@"^[A-Za-zА-Яа-я\s]*$").WithMessage("Book's genre must contain only letters or spaces");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Book's description is required")
                .Length(1, 500).WithMessage("Book's description must be between 1 and 500 characters long.");

            RuleFor(r => r.AuthorId)
                .NotEmpty().WithMessage("Author is required");
        }
    }
}
