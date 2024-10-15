using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllBookRentalsUseCase
    {
        public Task<IEnumerable<BookRentalResponse>> Execute(CancellationToken cancellationToken = default);
    }
}
