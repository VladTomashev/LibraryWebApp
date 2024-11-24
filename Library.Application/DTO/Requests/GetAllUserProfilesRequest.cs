using Library.Core.Entities;

namespace Library.Application.DTO.Requests
{
    public class GetAllUserProfilesRequest
    {
        public required PaginationParams PaginationParams { get; set; }
    }
}