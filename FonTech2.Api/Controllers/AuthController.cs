using FonTech2.Domain.Dto;
using FonTech2.Domain.Dto.User;
using FonTech2.Domain.Interfaces.Services;
using FonTech2.Domain.Result;
using Microsoft.AspNetCore.Mvc;


namespace FonTech2.Api.Controllers;
[ApiController]
public class AuthController : Controller
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    /// <summary>
    /// регистрация пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody] RegisterUserDto dto)
    {
        var response =await _authService.Register(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    /// <summary>
    /// логин пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody]LoginUserDto dto)
    {
        var response =await _authService.Login(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
  
}