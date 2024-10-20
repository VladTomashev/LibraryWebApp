using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

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

        public async Task<TokenResponse> Execute(TokenRequest request, CancellationToken cancellationToken = default)
        {
            string requestAccessToken = request.AccessToken;
            string requestRefreshToken = request.RefreshToken;

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
            string newRefreshToken = tokenService.GenerateRefreshToken();

            refreshToken.Token = newRefreshToken;
            await unitOfWork.RefreshTokenRepository.UpdateAsync(refreshToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new TokenResponse()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

        }
    }
}
