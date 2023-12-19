using onofrej.github.io;

namespace SampleWorker.IntegrationTests.Fixtures.Kafka;

internal sealed class KafkaFixture : IDisposable
{
    private readonly IAdminClient _adminClient;
    private readonly CachedSchemaRegistryClient _cachedSchemaRegistryClient;
    private readonly IConfiguration _configuration;
    private readonly IProducer<string, OrderEvent> _OrderProducer;
    private readonly string _orderTopic;

    public KafkaFixture(IConfiguration configuration)
    {
        _configuration = configuration;

        _orderTopic = _configuration.GetSection("KafkaSettings:Topics:Order").Value!;

        _cachedSchemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig
        {
            Url = _configuration.GetSection("KafkaSettings:SchemaRegistryUrl").Value,
            RequestTimeoutMs = 5000,
            MaxCachedSchemas = 10,
        });

        var avroSerializerValueConfig = new AvroSerializerConfig
        {
            AutoRegisterSchemas = true,
            SubjectNameStrategy = SubjectNameStrategy.TopicRecord,
        };

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = _configuration.GetSection("KafkaSettings:BootstrapServer").Value,
            CompressionType = CompressionType.Gzip,
            ClientId = _configuration.GetSection("KafkaSettings:ClientId").Value
        };

        _OrderProducer = new ProducerBuilder<string, OrderEvent>(producerConfig)
          .SetValueSerializer(new AvroSerializer<OrderEvent>(_cachedSchemaRegistryClient, avroSerializerValueConfig)
          .AsSyncOverAsync()).Build();

        _adminClient = new AdminClientBuilder(new AdminClientConfig
        {
            BootstrapServers = _configuration.GetSection("KafkaSettings:BootstrapServer").Value
        }).Build();

        CreateTopicsAsync().Wait();
    }

    public void Dispose()
    {
        DeleteTopicsAsync().Wait();

        _adminClient.Dispose();

        _cachedSchemaRegistryClient.Dispose();

        _OrderProducer.Dispose();
    }

    public void ProduceBrokeNotesMessage(OrderEvent message)
    {
        _OrderProducer.Produce(_orderTopic, new Message<string, OrderEvent> { Value = message });
        _OrderProducer.Flush();
    }

    private async Task CreateTopicsAsync()
    {
        var metadata = _adminClient.GetMetadata(TimeSpan.FromMilliseconds(100));

        if (!metadata.Topics.Exists(predicate => string.Equals(predicate.Topic, _orderTopic, StringComparison.OrdinalIgnoreCase)))
        {
            await _adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                new TopicSpecification { Name = _orderTopic, NumPartitions = 1, ReplicationFactor = 1
            } });
        }
    }

    private async Task DeleteTopicsAsync()
    {
        var metadata = _adminClient.GetMetadata(TimeSpan.FromMilliseconds(100));

        if (metadata.Topics.Exists(predicate => string.Equals(predicate.Topic, _orderTopic, StringComparison.OrdinalIgnoreCase)))
        {
            await _adminClient.DeleteTopicsAsync(new string[] { _orderTopic });
        }
    }
}