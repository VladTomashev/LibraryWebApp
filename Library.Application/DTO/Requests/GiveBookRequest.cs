using Library.Application.DTO.Basics;

namespace Library.Application.DTO.Requests
{
    public class GiveBookRequest
    {
        public required BookRentalDto BookRentalDto { get; set; }
    }
}