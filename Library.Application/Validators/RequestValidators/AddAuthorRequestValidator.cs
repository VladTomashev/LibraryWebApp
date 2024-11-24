using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Validators.DtoValidators;

namespace Library.Application.Validators.RequestValidators
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(r => r.AuthorDto)
            .NotNull().WithMessage("Author is required")
            .SetValidator(new AuthorDtoValidator());
        }
    }
}
