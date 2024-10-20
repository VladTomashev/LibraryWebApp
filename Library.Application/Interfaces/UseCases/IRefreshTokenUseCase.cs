using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IRefreshTokenUseCase
    {
        public Task<TokenResponse> Execute (TokenRequest request, CancellationToken cancellationToken = default);
    }
}
