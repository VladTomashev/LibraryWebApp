namespace Library.Application.DTO.Basics
{
    public class AuthorDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Country { get; set; }
    }
}
