using Library.Application.DTO.Basics;

namespace Library.Application.DTO.Requests
{
    public class AddAuthorRequest
    {
        public required AuthorDto AuthorDto { get; set; }
    }
}