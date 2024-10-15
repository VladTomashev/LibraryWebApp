using FluentValidation;
using FluentValidation.Results;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Services;

namespace Library.Application.Services
{
    public class ValidationService : IValidationService
    {
        public async Task ValidateAsync<T>(IValidator<T> validator, T request, CancellationToken cancellationToken = default)
        {
            ValidationResult result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new BadRequestException(errors);
            }
        }
    }
}
