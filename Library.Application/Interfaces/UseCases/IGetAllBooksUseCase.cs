using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllBooksUseCase
    {
        public Task<IEnumerable<BookResponse>> Execute(CancellationToken cancellationToken = default);
    }
}
