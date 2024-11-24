namespace Library.Application.DTO.Requests
{
    public class GetUserProfileByIdRequest
    {
        public required Guid UserId { get; set; }
    }
}