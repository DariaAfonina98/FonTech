using FonTech2.Domain.Entity;
using FonTech2.Domain.Result;

namespace FonTech2.Domain.Interfaces.Validations;

public interface IReportValidator : IBaseValidator<Report>
{
    /// <summary>
    /// Проверяется налиие отчета, если отчет с переданным названием есть в БД, то создать точно такой же нельзя
    /// Проверяется пользователь, если пользователь с UserId не найден, то такого пользователя нет
    /// </summary>
    /// <param name="report"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult CreateValidator(Report? report, User? user);
}