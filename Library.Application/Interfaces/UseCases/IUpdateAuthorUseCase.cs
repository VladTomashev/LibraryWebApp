using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IUpdateAuthorUseCase
    {
        public void Execute(AuthorUpdateRequest request, CancellationToken cancellationToken = default);
    }
}
