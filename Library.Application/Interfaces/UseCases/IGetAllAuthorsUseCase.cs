using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllAuthorsUseCase
    {
        public Task<IEnumerable<AuthorResponse>> Execute(PaginationParams paginationParams, 
            CancellationToken cancellationToken = default);
    }
}
