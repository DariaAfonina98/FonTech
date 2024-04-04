using AutoMapper;
using FonTech2.Domain.Dto.User;
using FonTech2.Domain.Entity;

namespace FonTech2.Application.Mapping;

public class UserMapping :Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}