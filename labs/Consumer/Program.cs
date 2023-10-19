using Confluent.Kafka;
using Confluent.SchemaRegistry;
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