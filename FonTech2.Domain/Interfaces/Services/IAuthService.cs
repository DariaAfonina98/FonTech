using FonTech2.Domain.Dto;
using FonTech2.Domain.Dto.User;
using FonTech2.Domain.Result;

namespace FonTech2.Domain.Interfaces.Services;


/// <summary>
/// Сервис, предназначенный для регистрации/авторизации
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserDto>> Register(RegisterUserDto dto);
    
    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
}