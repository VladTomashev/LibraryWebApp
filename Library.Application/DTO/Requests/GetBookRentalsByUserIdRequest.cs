using Library.Core.Entities;

namespace Library.Application.DTO.Requests
{
    public class GetBookRentalsByUserIdRequest
    {
        public required Guid UserId { get; set; }
        public required PaginationParams PaginationParams { get; set; }
    }
}