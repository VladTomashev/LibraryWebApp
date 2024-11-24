namespace Library.Application.DTO.Requests
{
    public class GetBookRentalByIdRequest
    {
        public required Guid BookRentalId { get; set; }
    }
}