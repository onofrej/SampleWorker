namespace SampleWorker.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddCreateOrderUseCase();

        services.AddScoped<IDynamoDBContext, DynamoDBContext>();
        services.AddAWSService<IAmazonDynamoDB>();

        return services;
    }
}