namespace Library.Application.DTO.Requests
{
    public class BookRequest
    {
        public required string Isbn { get; set; }
        public required string Name { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
        public required Guid AuthorId { get; set; }
        public string? ImagePath { get; set; }
    }

    public class BookUpdateRequest : BookRequest
    {
        public required Guid Id { get; set; }
    }
}
