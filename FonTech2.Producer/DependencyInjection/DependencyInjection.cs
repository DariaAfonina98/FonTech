using System.Runtime.CompilerServices;
using FonTech2.Producer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FonTech2.Producer.DependencyInjection;

public static class DependencyInjection
{
    public static void AddProducer(this IServiceCollection services)
    {
        services.AddScoped<IMessageProducer, Producer>();
    }
}