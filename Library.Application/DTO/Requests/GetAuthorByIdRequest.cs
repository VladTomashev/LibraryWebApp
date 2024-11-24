namespace Library.Application.DTO.Requests
{
    public class GetAuthorByIdRequest
    {
        public required Guid AuthorId { get; set; }
    }
}