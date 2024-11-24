using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetUserProfileByIdUseCase
    {
        public Task<UserProfileResponse> Execute (GetUserProfileByIdRequest request,
            CancellationToken cancellationToken = default);
    }
}
