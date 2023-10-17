namespace SampleWorker.Application.UseCases.CreateOrder;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCreateOrderUseCase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICreateOrderUseCase, CreateOrderUseCase>();

        return services;
    }
}