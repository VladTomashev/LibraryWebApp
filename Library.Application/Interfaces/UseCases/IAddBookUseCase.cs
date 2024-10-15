using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IAddBookUseCase
    {
        public void Execute(BookRequest request, CancellationToken cancellationToken = default);
    }
}
