using AutoMapper;
using FonTech2.Domain.Dto.Role;
using FonTech2.Domain.Entity;

namespace FonTech2.Application.Mapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
    }
}