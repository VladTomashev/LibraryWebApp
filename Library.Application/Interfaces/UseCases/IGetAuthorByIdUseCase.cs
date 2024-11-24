using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAuthorByIdUseCase
    {
        public Task<AuthorResponse> Execute(GetAuthorByIdRequest request, CancellationToken cancellationToken = default);
    }
}
