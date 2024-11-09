using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IRefreshTokenUseCase
    {
        public Task<string> Execute (TokenRequest request, CancellationToken cancellationToken = default);
    }
}
