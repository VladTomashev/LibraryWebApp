using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGiveBookUseCase
    {
        public Task Execute(GiveBookRequest request, CancellationToken cancellationToken = default);
    }
}
