using AutoMapper;
using FluentValidation;
using System.Security.Claims;
using Library.Application.DTO.Requests;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Library.Core.Enums;

namespace Library.Application.UseCases
{
    public class SignUpUseCase : ISignUpUseCase
    {
        private readonly ITokenService tokenService;
        private readonly IValidationService validationService;
        private readonly IValidator<SignUpRequest> validator;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PasswordHasher<UserAuth> passwordHasher;

        public SignUpUseCase(ITokenService tokenService, IValidationService validationService,
            IValidator<SignUpRequest> validator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.tokenService = tokenService;
            this.validationService = validationService;
            this.validator = validator;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            passwordHasher = new PasswordHasher<UserAuth>();
        }

        public async Task<TokenResponse> Execute(SignUpRequest request, CancellationToken cancellationToken = default)
        {
            await validationService.ValidateAsync(validator, request, cancellationToken);

            if (await unitOfWork.UserAuthRepository.GetByLoginAsync(request.Login, cancellationToken) != null)
            {
                throw new BadRequestException("Login is already in use");
            }

            UserAuth userAuth = new UserAuth
            {
                Id = Guid.NewGuid(),
                Login = request.Login,
                Role = Role.User
            };
            userAuth.PasswordHash = passwordHasher.HashPassword(userAuth, request.Password);

            UserProfile userProfile = mapper.Map<UserProfile>(request);
            userProfile.Id = userAuth.Id;

            RefreshToken refreshToken = new RefreshToken()
            {
                Id = userAuth.Id,
                LifeTime = DateTime.Now.AddDays(7),
                Token = tokenService.GenerateRefreshToken()
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAuth.Login),
                new Claim(ClaimTypes.Role, userAuth.Role.ToString())
            };
            string accessToken = tokenService.GenerateAccessToken(claims);

            await unitOfWork.UserAuthRepository.AddAsync(userAuth, cancellationToken);
            await unitOfWork.UserProfileRepository.AddAsync(userProfile, cancellationToken);
            await unitOfWork.RefreshTokenRepository.AddAsync(refreshToken, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
