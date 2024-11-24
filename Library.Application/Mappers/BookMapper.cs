using AutoMapper;
using Library.Application.DTO.Basics;
using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Mappers
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<Book, BookResponse>();
            CreateMap<BookDto, Book>();
            CreateMap<IEnumerable<Book>, IEnumerable<BookResponse>>()
                .ConvertUsing(books => books.Select(b => new BookResponse
                {
                    Id = b.Id,
                    Name = b.Name,
                    Isbn = b.Isbn,
                    Genre = b.Genre,
                    Description = b.Description,
                    AuthorId = b.AuthorId,
                    ImagePath = b.ImagePath
                }));
        }
    }
}
