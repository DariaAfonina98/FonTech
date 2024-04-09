using System.Net.Mime;
using FonTech2.Domain.Dto.Role;
using FonTech2.Domain.Entity;
using FonTech2.Domain.Interfaces.Services;
using FonTech2.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace FonTech2.Api.Controllers;

[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
[Route("api/[controller]")]

public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    ///  Создание роли
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Request for create report
    /// 
    ///POST
    /// 
    ///    {
    /// 
    ///    "name": "Admin",
    /// 
    ///    }
    /// 
    /// </remarks>
    /// <response code="200">если роль создалась</response>
    /// <response code="400">если роль не создалась</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Create([FromBody] RoleDto dto)
    {
        var response =await _roleService.CreateRoleAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    /// <summary>
    ///  Удаление роли с указанием идентификатора 
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Request for delete report
    ///
    ///   DELETE
    /// 
    ///     {
    /// 
    ///     "id": 1
    /// 
    ///     }
    /// </remarks>
    /// <response code="200">если роль удалилась</response>
    /// <response code="400">если роль не была удалена</response>
    [HttpDelete(nameof(id))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Delete( long id)
    {
        var response =await _roleService.DeleteRoleAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    
    /// <summary>
    ///  Обновление роли с указанием основных свойств
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Request for update report
    ///
    ///   PUT
    /// 
    ///     {
    /// 
    ///      "id": 1,
    /// 
    ///      "name": "Admin"
    /// 
    ///     }
    /// </remarks>
    /// <response code="200">если роль обновилась</response>
    /// <response code="400">если роль не была обновлена</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Update ([FromBody] RoleDto dto)
    {
        var response =await _roleService.UpdateRoleAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}