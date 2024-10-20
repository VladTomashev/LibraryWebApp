using AutoMapper;
using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Library.Application.UseCases
{
    public class SignInUseCase : ISignInUseCase
    {
        private readonly ITokenService tokenService;
        private readonly IValidationService validationService;
        private readonly IValidator<SignInRequest> validator;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PasswordHasher<UserAuth> passwordHasher;

        public SignInUseCase(ITokenService tokenService, IValidationService validationService,
            IValidator<SignInRequest> validator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.tokenService = tokenService;
            this.validationService = validationService;
            this.validator = validator;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            passwordHasher = new PasswordHasher<UserAuth>();
        }

        public async Task<TokenResponse> Execute(SignInRequest request, CancellationToken cancellationToken = default)
        {
            await validationService.ValidateAsync(validator, request, cancellationToken);

            UserAuth? userAuth = await unitOfWork.UserAuthRepository.GetByLoginAsync(request.Login, cancellationToken);
            if (userAuth == null)
            {
                throw new NotFoundException("User is not found");
            }

            var result = passwordHasher.VerifyHashedPassword(userAuth, userAuth.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success)
            {
                throw new BadRequestException("Wrong password");
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userAuth.Login),
                    new Claim(ClaimTypes.Role, userAuth.Role.ToString())
                };

                string accessTokenString = tokenService.GenerateAccessToken(claims);
                string refreshTokenString = tokenService.GenerateRefreshToken();

                RefreshToken refreshToken = await unitOfWork.RefreshTokenRepository
                    .GetByIdAsync(userAuth.Id, cancellationToken);
                refreshToken.Token = refreshTokenString;
                refreshToken.LifeTime = DateTime.Now.AddDays(7);
                await unitOfWork.RefreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                return new TokenResponse
                {
                    AccessToken = accessTokenString,
                    RefreshToken = refreshTokenString
                };
            }    
        }
    }
}
