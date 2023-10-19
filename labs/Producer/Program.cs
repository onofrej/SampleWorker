using AutoFixture;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Configuration;
using onofrej.github.io;

IAdminClient adminClient;
string orderTopic;
CachedSchemaRegistryClient cachedSchemaRegistryClient;
IConfiguration configuration;
IProducer<string, OrderEvent> orderProducer;

configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

orderTopic = configuration.GetSection("KafkaSettings:Topics:Order").Value!;

cachedSchemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig
{
    Url = configuration.GetSection("KafkaSettings:SchemaRegistryUrl").Value,
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
    BootstrapServers = configuration.GetSection("KafkaSettings:BootstrapServer").Value,
    CompressionType = CompressionType.Gzip,
    ClientId = configuration.GetSection("KafkaSettings:ClientId").Value
};

orderProducer = new ProducerBuilder<string, OrderEvent>(producerConfig)
  .SetValueSerializer(new AvroSerializer<OrderEvent>(cachedSchemaRegistryClient, avroSerializerValueConfig)
  .AsSyncOverAsync()).Build();

adminClient = new AdminClientBuilder(new AdminClientConfig
{
    BootstrapServers = configuration.GetSection("KafkaSettings:BootstrapServer").Value
}).Build();

var metadata = adminClient.GetMetadata(TimeSpan.FromMilliseconds(100));

if (!metadata.Topics.Exists(predicate => string.Equals(predicate.Topic, orderTopic, StringComparison.OrdinalIgnoreCase)))
{
    await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
        new TopicSpecification { Name = orderTopic, NumPartitions = 1, ReplicationFactor = 1
    } });
}

var length = 1000;

for (int counter = 0; counter < length; counter++)
{
    var message = new Fixture().Create<OrderEvent>();

    orderProducer.Produce(orderTopic, new Message<string, OrderEvent> { Value = message });
    orderProducer.Flush();

    Console.WriteLine(counter);
}

orderProducer.Dispose();