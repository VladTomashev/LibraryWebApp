using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllUserProfilesUseCase
    {
        public Task<IEnumerable<UserProfileResponse>> Execute (PaginationParams paginationParams, 
            CancellationToken cancellationToken = default);
    }
}
