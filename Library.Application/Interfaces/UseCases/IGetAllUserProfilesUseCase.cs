using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllUserProfilesUseCase
    {
        public Task<IEnumerable<UserProfileResponse>> Execute (GetAllUserProfilesRequest request, 
            CancellationToken cancellationToken = default);
    }
}
