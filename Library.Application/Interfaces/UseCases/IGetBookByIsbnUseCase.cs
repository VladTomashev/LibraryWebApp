using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetBookByIsbnUseCase
    {
        public Task<BookResponse> Execute(GetBookByIsbnRequest request, CancellationToken cancellationToken = default);
    }
}
