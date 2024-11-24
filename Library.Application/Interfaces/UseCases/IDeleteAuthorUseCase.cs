using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IDeleteAuthorUseCase
    {
        public Task Execute(DeleteAuthorRequest request, CancellationToken cancellationToken = default);
    }
}
