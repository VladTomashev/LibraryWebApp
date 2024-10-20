using FluentValidation;

namespace Library.Application.Interfaces.Services
{
    public interface IValidationService
    {
        Task ValidateAsync<T>(IValidator<T> validator, T request, CancellationToken cancellationToken = default);
    }
}
