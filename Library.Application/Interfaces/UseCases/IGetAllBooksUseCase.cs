using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllBooksUseCase
    {
        public Task<IEnumerable<BookResponse>> Execute(GetAllBooksRequest request,
            CancellationToken cancellationToken = default);
    }
}
