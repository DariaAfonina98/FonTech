using FluentValidation;
using FonTech2.Application.Mapping;
using FonTech2.Application.Services;
using FonTech2.Application.Validations;
using FonTech2.Application.Validations.FluentValidations.Report;
using FonTech2.Domain.Dto.Report;
using FonTech2.Domain.Interfaces.Services;
using FonTech2.Domain.Interfaces.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace FonTech2.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ReportMapping));
        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IReportValidator, ReportValidator>();
        services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
        services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
        
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        
    }
    
}