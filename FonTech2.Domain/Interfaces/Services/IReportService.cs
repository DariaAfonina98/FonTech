using FonTech2.Domain.Dto.Report;
using FonTech2.Domain.Result;

namespace FonTech2.Domain.Interfaces.Services;

/// <summary>
/// Сервис, отвечающий за работу с доменной частью отчета(Report)
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Получение всех отчетов пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ReportDto>> GetReportAsync(long userId);

    /// <summary>
    /// Получение отчета по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> GetReportByIdAsync(long id);
    
    /// <summary>
    /// Создание отчета с базовыми параметрами
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    
    Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto);
    
    
    /// <summary>
    /// Удаление отчета по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    
    Task<BaseResult<ReportDto>> DeleteReportAsync(long id);

    /// <summary>
    /// Обновление отчета
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    
    Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto);
}