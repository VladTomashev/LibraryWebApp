using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IUpdateAuthorUseCase
    {
        public Task Execute(AuthorUpdateRequest request, CancellationToken cancellationToken = default);
    }
}
