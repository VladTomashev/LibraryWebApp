using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IUpdateAuthorUseCase
    {
        public Task Execute(Guid id, AuthorRequest request, CancellationToken cancellationToken = default);
    }
}
