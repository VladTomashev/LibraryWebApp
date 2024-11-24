using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Application.Interfaces.UseCases;
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
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request,
            CancellationToken cancellationToken)
        {
            string response = await refreshTokenUseCase.Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAllUserProfiles([FromQuery] GetAllUserProfilesRequest request,
            CancellationToken cancellationToken)
        {
            IEnumerable<UserProfileResponse> response = await getAllUserProfilesUseCase
                .Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetUserProfileById([FromQuery] GetUserProfileByIdRequest request,
            CancellationToken cancellationToken)
        {
            UserProfileResponse response = await getUserProfileByIdUseCase.Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyUserProfile(CancellationToken cancellationToken)
        {
            Guid myId = await getIdByJwtUseCase
                .Execute(new GetIdByJwtRequest { Principal = HttpContext.User }, cancellationToken);

            GetUserProfileByIdRequest request = new GetUserProfileByIdRequest { UserId = myId };

            UserProfileResponse response = await getUserProfileByIdUseCase.Execute(request, cancellationToken);
            return Ok(response);
        }

    }
}
