using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGiveBookUseCase
    {
        public void Execute(BookRentalRequest request, CancellationToken cancellationToken = default);
    }
}
