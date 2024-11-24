using Library.Core.Entities;

namespace Library.Application.DTO.Requests
{
    public class GetUnavailableBooksRequest
    {
        public required PaginationParams PaginationParams { get; set; }
    }
}