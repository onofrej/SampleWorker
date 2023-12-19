using SampleWorker.Application.UseCases.CreateOrder.DataAccess;

namespace SampleWorker.Application.UseCases.CreateOrder;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCreateOrderUseCase(this IServiceCollection services)
    {
        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IOrderData, OrderData>();

        return services;
    }
}