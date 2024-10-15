using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetBookRentalByIdUseCase
    {
        public Task<BookRentalResponse> Execute (Guid id, CancellationToken cancellationToken = default);
    }
}
