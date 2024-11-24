using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IAddBookUseCase
    {
        public Task Execute(AddBookRequest request, CancellationToken cancellationToken = default);
    }
}
