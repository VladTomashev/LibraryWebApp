using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IDeleteBookUseCase
    {
        public Task Execute(DeleteBookRequest request, CancellationToken cancellationToken = default);
    }
}
