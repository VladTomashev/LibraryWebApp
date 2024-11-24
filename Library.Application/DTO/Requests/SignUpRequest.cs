namespace Library.Application.DTO.Requests
{
    public class SignUpRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
    }
}