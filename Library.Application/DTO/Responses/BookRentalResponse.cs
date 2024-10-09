namespace Library.Application.DTO.Responses
{
    public class BookRentalResponse
    {
        public required Guid Id { get; set; }
        public required Guid BookId { get; set; }
        public required Guid UserProfileId { get; set; }
        public required DateTime TakingTime { get; set; }
        public required DateTime ReturnTime { get; set; }
        public required bool IsReturned { get; set; }
    }
}
