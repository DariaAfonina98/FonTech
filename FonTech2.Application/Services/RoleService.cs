using FonTech2.Application.Resourses;
using FonTech2.Domain.Dto.Role;
using FonTech2.Domain.Entity;
using FonTech2.Domain.Enum;
using FonTech2.Domain.Interfaces.Services;
using FonTech2.Domain.Repositories;
using FonTech2.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace FonTech2.Application.Services;

public class RoleService : IRoleService
{
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly IBaseRepository<User> _userRepository;

    public RoleService(IBaseRepository<Role> roleRepository, IBaseRepository<User> userRepositiry)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepositiry;
    }

    public async Task<BaseResult<Role>> CreateRoleAsync(RoleDto dto)
    {

        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);

        if (role != null)
        {
            return new BaseResult<Role>()
            {
                ErrorMessage = ErrorMessage.RoleAlreadyExists,
                ErrorCode =(int) ErrorCodes.RoleAlreadyExists

            };
            
        }
        role = new Role()
        {
           Name = dto.Name
        };
        await _roleRepository.CreateAsync(role);
        return new BaseResult<Role>()
        {
           Data = role
        };
        
    }

    public async Task<BaseResult<Role>> DeleteRoleAsync(long id)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        if (role == null)
        {
            return new BaseResult<Role>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode =(int) ErrorCodes.RoleNotFound

            };
        }

        await _roleRepository.RemoveAsync(role);

        return new BaseResult<Role>()
        {
            Data = role
        };
      
    }

    public async Task<BaseResult<Role>> UpdateRoleAsync(RoleDto dto)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (role == null)
        {
            return new BaseResult<Role>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode =(int) ErrorCodes.RoleNotFound

            };
        }

        role.Name = dto.Name;
        await _roleRepository.UpdateAsync(role);

        return new BaseResult<Role>()
        {
            Data = role
        };
    }
}