using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface ISignUpUseCase
    {
        public Task<TokenResponse> Execute(SignUpRequest request, CancellationToken cancellationToken = default);
    }
}
