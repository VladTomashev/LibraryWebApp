using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllBooksUseCase
    {
        public Task<IEnumerable<BookResponse>> Execute(PaginationParams paginationParams,
            CancellationToken cancellationToken = default);
    }
}
