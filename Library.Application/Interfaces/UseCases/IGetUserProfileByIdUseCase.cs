using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetUserProfileByIdUseCase
    {
        public Task<UserProfileResponse> Execute (Guid id, CancellationToken cancellationToken = default);
    }
}
