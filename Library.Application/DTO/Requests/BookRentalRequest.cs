namespace Library.Application.DTO.Requests
{
    public class BookRentalRequest
    {
        public required Guid BookId { get; set; }
        public required Guid UserProfileId { get; set; }
        public required DateTime TakingTime { get; set; }
        public required DateTime ReturnTime { get; set; }
    }
}
