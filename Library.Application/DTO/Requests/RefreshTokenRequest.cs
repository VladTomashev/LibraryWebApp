using Library.Application.DTO.Basics;

namespace Library.Application.DTO.Requests
{
    public class RefreshTokenRequest
    {
        public required TokenDto TokenDto { get; set; }
    }
}