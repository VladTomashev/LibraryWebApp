using Library.Application.DTO.Requests;

namespace Library.Application.Interfaces.UseCases
{
    public interface IUpdateBookUseCase
    {
        public void Execute(BookUpdateRequest request, CancellationToken cancellationToken = default);
    }
}
