using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

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

        public async Task<UserProfileResponse> Execute(GetUserProfileByIdRequest request,
            CancellationToken cancellationToken = default)
        {
            UserProfile? userProfile = await unitOfWork.UserProfileRepository
                .GetByIdAsync(request.UserId, cancellationToken);
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
