using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Core.Entities;

namespace Library.Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<IEnumerable<UserProfile>, IEnumerable<UserProfileResponse>>()
                .ConvertUsing(profiles => profiles.Select(p => new UserProfileResponse
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone
                }));
        }
    }
}
