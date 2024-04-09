using Asp.Versioning;
using FonTech2.Domain.Dto.Report;
using FonTech2.Domain.Interfaces.Services;
using FonTech2.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FonTech2.Api.Controllers;
[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }
    
    /// <summary>
    ///  Получение всех отчетов пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <remarks>
    ///  Get user reports
    ///
    ///    GET
    /// 
    ///      {
    /// 
    ///      "userId": 1
    /// 
    ///      } 
    /// </remarks>
    /// <response code="200">если отчет создался</response>
    [HttpGet(nameof(userId))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetUserReports(long userId)
    {
        var response =await _reportService.GetReportAsync(userId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    
    /// <summary>
    ///  Получение отчета по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Request for get report
    ///
    ///    GET
    /// 
    ///     {
    /// 
    ///      "id": 1
    /// 
    ///     }
    /// </remarks>
    /// <response code="200">если отчет создался</response>
    [HttpGet(nameof(id))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(long id)
    {
        var response =await _reportService.GetReportByIdAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    ///  Удаление отчета
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
    /// <response code="200">если отчет удалился</response>
    /// <response code="400">если отчет не был удален</response>
    [HttpDelete(nameof(id))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Delete(long id)
    {
        var response =await _reportService.DeleteReportAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    /// <summary>
    ///  Создание отчета
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Request for create report
    /// 
    ///POST
    /// 
    ///    {
    /// 
    ///    "name": "report#1",
    /// 
    ///    "description": "test report",
    /// 
    ///    "userId": 1
    /// 
    ///    }
    /// 
    /// </remarks>
    /// <response code="200">если отчет создался</response>
    /// <response code="400">если отчет не был создан</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Create([FromBody]CreateReportDto dto)
    {
        var response =await _reportService.CreateReportAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    /// <summary>
    ///  Обновление отчета
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
    ///      "name": "Report #2",
    /// 
    ///      "description": "Test report #2"
    /// 
    ///     }
    /// </remarks>
    /// <response code="200">если отчет обновился</response>
    /// <response code="400">если отчет не был обновлен</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Update([FromBody] UpdateReportDto dto)
    {
        var response =await _reportService.UpdateReportAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}