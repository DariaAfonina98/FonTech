using FonTech2.Domain.Dto;
using FonTech2.Domain.Interfaces.Services;
using FonTech2.Domain.Result;
using Microsoft.AspNetCore.Mvc;


namespace FonTech2.Api.Controllers;

[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    
    [HttpPost]
    [Route("refresh")]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken([FromBody] TokenDto tokenDto)
    {
        var response =await _tokenService.RefreshToken(tokenDto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}