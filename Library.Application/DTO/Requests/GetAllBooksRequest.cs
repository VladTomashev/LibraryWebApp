using Library.Core.Entities;

namespace Library.Application.DTO.Requests
{
    public class GetAllBooksRequest
    {
        public required PaginationParams PaginationParams { get; set; }
    }
}