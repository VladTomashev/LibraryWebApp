using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetBookRentalByIdUseCase
    {
        public Task<BookRentalResponse> Execute (GetBookRentalByIdRequest request, CancellationToken cancellationToken = default);
    }
}
