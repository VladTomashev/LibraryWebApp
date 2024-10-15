namespace Library.Application.DTO.Responses
{
    public class BookResponse
    {
        public required Guid Id { get; set; }
        public required string Isbn { get; set; }
        public required string Name { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
        public required Guid AuthorId { get; set; }
        public string? ImagePath { get; set; }
    }
}
