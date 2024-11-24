using Library.Core.Entities;

namespace Library.Application.DTO.Requests
{
    public class GetAllBookRentalsRequest
    {
        public required PaginationParams PaginationParams { get; set; }
    }
}