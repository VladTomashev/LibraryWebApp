namespace Library.Application.DTO.Responses
{
    public class AuthorResponse
    {
        public required Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Country { get; set; }
    }
}
