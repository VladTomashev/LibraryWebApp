using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IReturnBookUseCase
    {
        public Task Execute(ReturnBookRequest request, CancellationToken cancellationToken = default);
    }
}
