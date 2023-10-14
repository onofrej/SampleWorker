using SampleWorker.Application.DependencyInjection;
using SampleWorker.Worker.Base;
using SampleWorker.Worker.Consumers.Orders;

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
        services.AddScopedHostedService<OrderConsumer>();

        return services;
    }

    private static IServiceCollection AddScopedHostedService<T>(this IServiceCollection services) where T : IScopedBackgroundService
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        services.AddHostedService<ScopedBackgroundService<T>>();
        services.AddScoped(typeof(T));

        return services;
    }
}