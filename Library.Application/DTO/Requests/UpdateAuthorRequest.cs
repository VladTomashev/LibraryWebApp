using Library.Application.DTO.Basics;

namespace Library.Application.DTO.Requests
{
    public class UpdateAuthorRequest
    {
        public required Guid AuthorId { get; set; }
        public required AuthorDto AuthorDto { get; set; }
    }
}