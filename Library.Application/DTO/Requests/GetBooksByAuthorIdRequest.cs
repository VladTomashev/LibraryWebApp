using Library.Core.Entities;

namespace Library.Application.DTO.Requests
{
    public class GetBooksByAuthorIdRequest
    {
        public required Guid AuthorId { get; set; }
        public required PaginationParams PaginationParams { get; set; }
    }
}