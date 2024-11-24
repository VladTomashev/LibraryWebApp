using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetBookRentalsByUserIdUseCase
    {
        public Task<IEnumerable<BookRentalResponse>> Execute(GetBookRentalsByUserIdRequest request,
            CancellationToken cancellationToken = default);
    }
}
