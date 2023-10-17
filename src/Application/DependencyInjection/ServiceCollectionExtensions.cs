using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using SampleWorker.Application.UseCases.CreateOrder;

namespace SampleWorker.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCreateOrderUseCase(configuration);

        services.AddScoped<IDynamoDBContext, DynamoDBContext>();
        services.AddAWSService<IAmazonDynamoDB>();

        return services;
    }
}