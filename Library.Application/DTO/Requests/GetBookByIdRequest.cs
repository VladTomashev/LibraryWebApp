namespace Library.Application.DTO.Requests
{
    public class GetBookByIdRequest
    {
        public required Guid BookId { get; set; }
    }
}