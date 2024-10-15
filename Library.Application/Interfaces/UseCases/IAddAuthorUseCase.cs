using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IAddAuthorUseCase
    {
        public void Execute(AuthorRequest request, CancellationToken cancellationToken = default);
    }
}
