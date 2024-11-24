using Library.Application.DTO.Requests;
using System.Security.Claims;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetIdByJwtUseCase
    {
        public Task<Guid> Execute(GetIdByJwtRequest request, CancellationToken cancellationToken = default);
    }
}
