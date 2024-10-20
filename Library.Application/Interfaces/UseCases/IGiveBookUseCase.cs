using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGiveBookUseCase
    {
        public Task Execute(BookRentalRequest request, CancellationToken cancellationToken = default);
    }
}
