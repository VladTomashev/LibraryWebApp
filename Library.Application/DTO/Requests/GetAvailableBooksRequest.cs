using Library.Core.Entities;

namespace Library.Application.DTO.Requests
{
    public class GetAvailableBooksRequest
    {
        public required PaginationParams PaginationParams { get; set; }
    }
}