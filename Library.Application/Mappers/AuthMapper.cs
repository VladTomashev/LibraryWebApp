using AutoMapper;
using Library.Application.DTO.Requests;
using Library.Core.Entities;

namespace Library.Application.Mappers
{
    public class AuthMapper : Profile
    {
        public AuthMapper()
        {
            CreateMap<SignUpRequest, UserProfile>();
            CreateMap<SignUpRequest, UserAuth>();
        }
    }
}
