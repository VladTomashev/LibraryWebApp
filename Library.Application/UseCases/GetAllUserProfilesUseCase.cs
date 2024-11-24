using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

namespace Library.Application.UseCases
{
    public class GetAllUserProfilesUseCase : IGetAllUserProfilesUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllUserProfilesUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserProfileResponse>> Execute(GetAllUserProfilesRequest request, 
            CancellationToken cancellationToken = default)
        {
            IEnumerable<UserProfile>? userProfiles = await unitOfWork.UserProfileRepository
                .GetAllAsync(request.PaginationParams, cancellationToken);
            if (!userProfiles.Any())
            {
                throw new NotFoundException("Users not found");
            }
            else
            {
                IEnumerable<UserProfileResponse> response = mapper.Map<IEnumerable<UserProfileResponse>>(userProfiles);
                return response;
            }
        }
    }
}
