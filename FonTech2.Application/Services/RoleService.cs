using AutoMapper;
using FonTech2.Application.Resourses;
using FonTech2.Domain.Dto.Role;
using FonTech2.Domain.Dto.UserRole;
using FonTech2.Domain.Entity;
using FonTech2.Domain.Enum;
using FonTech2.Domain.Interfaces.Databases;
using FonTech2.Domain.Interfaces.Services;
using FonTech2.Domain.Interfaces.Repositories;
using FonTech2.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace FonTech2.Application.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<UserRole> _userRoleRepository;
    private readonly IMapper _mapper;

    public RoleService(IBaseRepository<Role> roleRepository, IBaseRepository<User> userRepositiry, IMapper mapper, IBaseRepository<UserRole> userRoleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepositiry;
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto)
    {

        var role = await _roleRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Name == dto.Name);

        if (role != null)
        {
            return new BaseResult<RoleDto>()
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
        return new BaseResult<RoleDto>()
        {
           Data = _mapper.Map<RoleDto>(role)
        };
        
    }

    public async Task<BaseResult<RoleDto>> DeleteRoleAsync(long id)
    {
        var role = await _roleRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Id == id);
        if (role == null)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode =(int) ErrorCodes.RoleNotFound

            };
        }
        _roleRepository.Remove(role);
        await _roleRepository.SaveChangesAsync();

        return new BaseResult<RoleDto>()
        {
            Data = _mapper.Map<RoleDto>(role)
        };
      
    }

    public async Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto)
    {
        var role = await _roleRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (role == null)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode =(int) ErrorCodes.RoleNotFound

            };
        }

        role.Name = dto.Name;
        var updatedRole = _roleRepository.Update(role);
        await _roleRepository.SaveChangesAsync();

        return new BaseResult<RoleDto>()
        {
            Data = _mapper.Map<RoleDto>(updatedRole)
        };
    }

    public async Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto)
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);
        
        if (user == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode =(int) ErrorCodes.UserNotFound

            };
        }

        var roles = user.Roles.Select(x => x.Name).ToArray();

        if (roles.All(x => x != dto.RoleName))
        {
            var role = await _roleRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Name == dto.RoleName);
            if (role == null)
            {
                return new BaseResult<UserRoleDto>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode =(int) ErrorCodes.RoleNotFound

                };
            }
            UserRole userRole = new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            };

            await _userRoleRepository.CreateAsync(userRole);

            return new BaseResult<UserRoleDto>()
            {
                Data = new UserRoleDto()
                {
                    RoleName = role.Name,
                    Login = user.Login
                }
            };
        }

        return new BaseResult<UserRoleDto>()
        {
            ErrorMessage = ErrorMessage.UserAlreadyExistsThisRole,
            ErrorCode = (int)ErrorCodes.UserAlreadyExistsThisRole
        };
    }

    public async Task<BaseResult<UserRoleDto>> DeleteRoleForUserAsync(DeleteUserRoleDto dto)
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);
        
        if (user == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode =(int) ErrorCodes.UserNotFound

            };
        }

        var role = user.Roles.FirstOrDefault(x => x.Id == dto.RoleId);
        if (role == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode =(int) ErrorCodes.RoleNotFound

            };
        }

        var userRole = await _userRoleRepository.GetAll()
            .Where(x => x.RoleId == role.Id)
            .FirstOrDefaultAsync(x => x.UserId == user.Id);
        
        _userRoleRepository.Remove(userRole);
        await _userRoleRepository.SaveChangesAsync();
        
        return new BaseResult<UserRoleDto>()
        {
            Data = new UserRoleDto()
            {
                RoleName = role.Name,
                Login = user.Login
            }
        };
    }

    public async Task<BaseResult<UserRoleDto>> UpdateRoleForUserAsync(UpdateUserRoleDto dto)
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);
        
        if (user == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode =(int) ErrorCodes.UserNotFound

            };
        }

        var role = user.Roles.FirstOrDefault(x => x.Id == dto.FromRoleId);
        if (role == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode =(int) ErrorCodes.RoleNotFound

            };
        }

        var newRoleForUser = await _roleRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Id == dto.ToRoleId);
        
        if (newRoleForUser == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode =(int) ErrorCodes.RoleNotFound

            };
        }

        using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                var userRole = await _unitOfWork.UserRoles.GetAll()
                    .Where(x => x.RoleId == role.Id)
                    .FirstAsync(x => x.UserId == user.Id);

                _unitOfWork.UserRoles.Remove(userRole);
                await _unitOfWork.SaveChangesAsync();


                var newUserRole = new UserRole()
                {
                    UserId = user.Id,
                    RoleId = newRoleForUser.Id
                };
                
                await _unitOfWork.UserRoles.CreateAsync(newUserRole);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception )
            {
                await transaction.RollbackAsync();
            }
           
        }
        
        return new BaseResult<UserRoleDto>()
        {
            Data = new UserRoleDto()
            {
                RoleName = newRoleForUser.Name,
                Login = user.Login
            }
        };
    }
}