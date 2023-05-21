using SampleWorker.Application.DependencyInjection;

namespace SampleWorker.Worker.DependencyInjection;

[ExcludeFromCodeCoverage]
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InitializeAppliactionServices(this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddUseCases(configuration)
            .AddConsumers();

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services)
    {
        services.AddHostedService<Worker>();

        return services;
    }
}