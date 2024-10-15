using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetBookByIdUseCase
    {
        public Task<BookResponse> Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
