using Library.Application.DTO.Basics;

namespace Library.Application.DTO.Requests
{
    public class AddBookRequest
    {
        public required BookDto BookDto { get; set; }
    }
}