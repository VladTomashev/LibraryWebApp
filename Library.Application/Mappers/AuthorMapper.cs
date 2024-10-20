using AutoMapper;
using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Mappers
{
    public class AuthorMapper : Profile
    {
        public AuthorMapper()
        {
            CreateMap<Author, AuthorResponse>();
            CreateMap<AuthorRequest, Author>();
            CreateMap<IEnumerable<Author>, IEnumerable<AuthorResponse>>()
                .ConvertUsing(authors => authors.Select(a => new AuthorResponse
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Country = a.Country,
                    DateOfBirth = a.DateOfBirth
                }));
        }
    }
}
