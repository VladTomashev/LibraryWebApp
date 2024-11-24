using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IRefreshTokenUseCase
    {
        public Task<string> Execute (RefreshTokenRequest request, CancellationToken cancellationToken = default);
    }
}
