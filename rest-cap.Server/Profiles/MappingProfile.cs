using AutoMapper;
using rest_cap.Server.Models;

namespace rest_cap.Server.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
