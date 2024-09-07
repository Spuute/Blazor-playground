using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class ConfigureServices
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}