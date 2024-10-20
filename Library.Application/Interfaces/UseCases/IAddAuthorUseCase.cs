using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IAddAuthorUseCase
    {
        public Task Execute(AuthorRequest request, CancellationToken cancellationToken = default);
    }
}
