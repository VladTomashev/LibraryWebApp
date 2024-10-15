using System.Security.Claims;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetIdByJwtUseCase
    {
        public Task<Guid> Execute(ClaimsPrincipal principal, CancellationToken cancellationToken = default);
    }
}
