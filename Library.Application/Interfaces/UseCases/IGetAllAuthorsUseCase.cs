using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllAuthorsUseCase
    {
        public Task<IEnumerable<AuthorResponse>> Execute(CancellationToken cancellationToken = default);
    }
}
