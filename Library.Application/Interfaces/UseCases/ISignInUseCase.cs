using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface ISignInUseCase
    {
        public Task<TokenResponse> Execute(SignInRequest request, CancellationToken cancellationToken = default);
    }
}
