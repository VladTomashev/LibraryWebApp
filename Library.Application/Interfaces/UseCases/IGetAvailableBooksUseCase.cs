using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAvailableBooksUseCase
    {
        public Task<IEnumerable<BookResponse>> Execute(GetAvailableBooksRequest request,
            CancellationToken cancellationToken = default);
    }
}
