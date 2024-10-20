using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetBooksByAuthorIdUseCase
    {
        public Task<IEnumerable<BookResponse>> Execute(Guid id, PaginationParams paginationParams,
            CancellationToken cancellationToken = default);
    }
}
