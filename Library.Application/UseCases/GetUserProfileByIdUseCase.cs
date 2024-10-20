using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

namespace Library.Application.UseCases
{
    public class GetUserProfileByIdUseCase : IGetUserProfileByIdUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetUserProfileByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<UserProfileResponse> Execute(Guid id, CancellationToken cancellationToken = default)
        {
            UserProfile? userProfile = await unitOfWork.UserProfileRepository.GetByIdAsync(id, cancellationToken);
            if (userProfile == null)
            {
                throw new NotFoundException("User not found");
            }
            else
            {
                UserProfileResponse response = mapper.Map<UserProfileResponse>(userProfile);
                return response;
            }
        }
    }
}
