using System.Security.Claims;

namespace Library.Application.DTO.Requests
{
    public class GetIdByJwtRequest
    {
        public required ClaimsPrincipal Principal { get; set; }
    }
}