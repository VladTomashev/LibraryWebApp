using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetBookByIdUseCase
    {
        public Task<BookResponse> Execute(GetBookByIdRequest request, CancellationToken cancellationToken = default);
    }
}
