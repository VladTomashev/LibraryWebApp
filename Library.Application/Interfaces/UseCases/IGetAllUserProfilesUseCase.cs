using Library.Application.DTO.Responses;

namespace Library.Application.Interfaces.UseCases
{
    public interface IGetAllUserProfilesUseCase
    {
        public Task<IEnumerable<UserProfileResponse>> Execute (CancellationToken cancellationToken = default);
    }
}
