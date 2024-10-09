namespace Library.Core.Entities
{
    public class BookRental : AbstractEntity
    {
        public Guid BookId { get; set; }
        public Guid UserProfileId { get; set; }
        public DateTime TakingTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public bool IsReturned { get; set; }
        
    }
}
