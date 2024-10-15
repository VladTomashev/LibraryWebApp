using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetBookByIsbnUseCase
    {
        public Task<BookResponse> Execute(string isbn, CancellationToken cancellationToken = default);
    }
}
