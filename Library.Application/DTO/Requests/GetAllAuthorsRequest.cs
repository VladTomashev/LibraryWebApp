using Library.Core.Entities;

namespace Library.Application.DTO.Requests
{
    public class GetAllAuthorsRequest
    {
        public required PaginationParams PaginationParams { get; set; }
    }
}