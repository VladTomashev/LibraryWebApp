using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetBookRentalsByUserIdUseCase
    {
        public Task<IEnumerable<BookRentalResponse>> Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
