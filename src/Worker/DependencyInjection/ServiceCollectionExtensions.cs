using onofrej.github.io;
using SampleWorker.Application.DependencyInjection;
using SampleWorker.Worker.Base;
using SampleWorker.Worker.Consumers.Orders;

namespace SampleWorker.Worker.DependencyInjection;

[ExcludeFromCodeCoverage]
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InitializeAppliactionServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddUseCases()
            .AddConsumers(configuration);

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services,
      IConfiguration configuration)
    {
        services.AddScopedHostedService<OrderConsumer>();

        string brokerNotesTopic = configuration.GetSection("kafka:Consumer:Topics:Order").Value!;

        var schemaRegistryConfig = new SchemaRegistryConfig
        {
            Url = configuration.GetSection("kafka:SchemaRegistryUrl").Value
        };

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration.GetSection("kafka:BootstrapServer").Value,
            ClientId = configuration.GetSection("kafka:Consumer:ClientId").Value,
            GroupId = configuration.GetSection("kafka:Consumer:GroupId").Value,
            EnableAutoCommit = true,
            EnableAutoOffsetStore = false,
            AutoCommitIntervalMs = 1000,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        services.AddSingleton(services =>
        {
            var brokerNotesConsumer = new ConsumerBuilder<string, OrderEvent>(consumerConfig)
              .SetValueDeserializer(new AvroDeserializer<OrderEvent>(new CachedSchemaRegistryClient(schemaRegistryConfig)).AsSyncOverAsync())
              .Build();

            brokerNotesConsumer.Subscribe(brokerNotesTopic);

            return brokerNotesConsumer;
        });

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