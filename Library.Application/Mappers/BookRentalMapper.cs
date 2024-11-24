using AutoMapper;
using Library.Application.DTO.Basics;
using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Mappers
{
    public class BookRentalMapper : Profile
    {
        public BookRentalMapper()
        {
            CreateMap<BookRental, BookRentalResponse>();
            CreateMap<BookRentalDto, BookRental>();
            CreateMap<IEnumerable<BookRental>, IEnumerable<BookRentalResponse>>()
                .ConvertUsing(rentals => rentals.Select(br => new BookRentalResponse
                {
                    Id = br.Id,
                    BookId = br.BookId,
                    UserProfileId = br.UserProfileId,
                    TakingTime = br.TakingTime,
                    ReturnTime = br.ReturnTime,
                    IsReturned = br.IsReturned
                }));
        }
    }
}
