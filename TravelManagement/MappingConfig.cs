using AutoMapper;
using TravelManagement.Models;
using TravelManagement.Models.Dto;

namespace TravelManagement
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<User, UserUpdateDTO>().ReverseMap();
        }
    }
}
