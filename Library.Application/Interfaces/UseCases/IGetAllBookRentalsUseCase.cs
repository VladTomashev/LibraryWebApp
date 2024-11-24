using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllBookRentalsUseCase
    {
        public Task<IEnumerable<BookRentalResponse>> Execute(GetAllBookRentalsRequest request, 
            CancellationToken cancellationToken = default);
    }
}
