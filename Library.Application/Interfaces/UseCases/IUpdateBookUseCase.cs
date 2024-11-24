using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IUpdateBookUseCase
    {
        public Task Execute(UpdateBookRequest request, CancellationToken cancellationToken = default);
    }
}
