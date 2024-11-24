using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IUpdateAuthorUseCase
    {
        public Task Execute(UpdateAuthorRequest request, CancellationToken cancellationToken = default);
    }
}
