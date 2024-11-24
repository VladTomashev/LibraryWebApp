using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Validators.DtoValidators;

namespace Library.Application.Validators.RequestValidators
{
    public class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorRequestValidator()
        {
            RuleFor(r => r.AuthorId)
            .NotEmpty().WithMessage("Id is required.");

            RuleFor(r => r.AuthorDto)
            .NotNull().WithMessage("Author is required")
            .SetValidator(new AuthorDtoValidator());
        }
    }
}
