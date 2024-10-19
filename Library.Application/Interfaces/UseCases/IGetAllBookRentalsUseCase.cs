using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllBookRentalsUseCase
    {
        public Task<IEnumerable<BookRentalResponse>> Execute(PaginationParams paginationParams, 
            CancellationToken cancellationToken = default);
    }
}
