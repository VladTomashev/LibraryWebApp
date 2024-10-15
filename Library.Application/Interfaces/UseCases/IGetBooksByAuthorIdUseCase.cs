using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetBooksByAuthorIdUseCase
    {
        public Task<IEnumerable<BookResponse>> Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
