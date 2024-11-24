using Library.Application.DTO.Requests;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.Interfaces.Services;

namespace Library.Application.UseCases
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly ITokenService tokenService;
        private readonly IUnitOfWork unitOfWork;

        public RefreshTokenUseCase(ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> Execute(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            string requestAccessToken = request.TokenDto.AccessToken;
            string requestRefreshToken = request.TokenDto.RefreshToken;

            var principal = tokenService.GetPrincipalFromExpiredToken(requestAccessToken);
            string login = principal.Identity.Name;

            UserAuth userAuth = await unitOfWork.UserAuthRepository.GetByLoginAsync(login, cancellationToken);
            if (userAuth == null)
            {
                throw new NotFoundException("User is not found");
            }

            RefreshToken refreshToken = await unitOfWork.RefreshTokenRepository.GetByIdAsync(userAuth.Id, cancellationToken);
            if (refreshToken.Token != requestRefreshToken)
            {
                throw new BadRequestException("Incorrect refresh token");
            }
                
            if (refreshToken.LifeTime <= DateTime.Now)
            {
                throw new BadRequestException("The token has expired, please log in again");
            }
                
            string newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            return newAccessToken;

        }
    }
}
