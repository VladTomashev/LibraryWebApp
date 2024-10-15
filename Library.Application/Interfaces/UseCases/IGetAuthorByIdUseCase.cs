using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAuthorByIdUseCase
    {
        public Task<AuthorResponse> Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
