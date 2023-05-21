using SampleWorker.Application.UseCases.CreateOrder;

namespace SampleWorker.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCreateOrderCase(configuration);

        return services;
    }
}