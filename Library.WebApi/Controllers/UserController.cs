using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private ISignInUseCase signInUseCase;
        private ISignUpUseCase signUpUseCase;
        private IGetIdByJwtUseCase getIdByJwtUseCase;
        private IRefreshTokenUseCase refreshTokenUseCase;
        private IGetAllUserProfilesUseCase getAllUserProfilesUseCase;
        private IGetUserProfileByIdUseCase getUserProfileByIdUseCase;

        public UserController(ISignInUseCase signInUseCase, ISignUpUseCase signUpUseCase,
            IGetIdByJwtUseCase getIdByJwtUseCase, IRefreshTokenUseCase refreshTokenUseCase,
            IGetAllUserProfilesUseCase getAllUserProfilesUseCase,
            IGetUserProfileByIdUseCase getUserProfileByIdUseCase)
        {
            this.signInUseCase = signInUseCase;
            this.signUpUseCase = signUpUseCase;
            this.getIdByJwtUseCase = getIdByJwtUseCase;
            this.refreshTokenUseCase = refreshTokenUseCase;
            this.getAllUserProfilesUseCase = getAllUserProfilesUseCase;
            this.getUserProfileByIdUseCase = getUserProfileByIdUseCase;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request,
            CancellationToken cancellationToken)
        {
            TokenResponse response = await signInUseCase.Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request,
            CancellationToken cancellationToken)
        {
            TokenResponse response = await signUpUseCase.Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("refreshtoken")]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest request, CancellationToken cancellationToken)
        {
            TokenResponse response = await refreshTokenUseCase.Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> GetAllUserProfiles([FromQuery] PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            IEnumerable<UserProfileResponse> response = await getAllUserProfilesUseCase
                .Execute(paginationParams,cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> GetUserProfileById(Guid id, CancellationToken cancellationToken)
        {
            UserProfileResponse response = await getUserProfileByIdUseCase.Execute(id, cancellationToken);
            return Ok(response);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyUserProfile(CancellationToken cancellationToken)
        {
            Guid myId = await getIdByJwtUseCase.Execute(HttpContext.User, cancellationToken);
            UserProfileResponse response = await getUserProfileByIdUseCase.Execute(myId, cancellationToken);
            return Ok(response);
        }

    }
}
