using Library.Application.DTO.Basics;

namespace Library.Application.DTO.Requests
{
    public class UpdateBookRequest
    {
        public required Guid BookId { get; set; }
        public required BookDto BookDto { get; set; }
    }
}