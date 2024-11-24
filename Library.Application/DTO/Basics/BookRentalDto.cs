namespace Library.Application.DTO.Basics
{
    public class BookRentalDto
    {
        public required Guid BookId { get; set; }
        public required Guid UserProfileId { get; set; }
        public required DateTime TakingTime { get; set; }
        public required DateTime ReturnTime { get; set; }
    }
}
