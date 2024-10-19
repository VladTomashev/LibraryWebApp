using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IUpdateBookUseCase
    {
        public Task Execute(Guid id, BookRequest request, CancellationToken cancellationToken = default);
    }
}
