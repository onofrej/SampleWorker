using System.Diagnostics;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Consumer;
using Microsoft.Extensions.Configuration;
using onofrej.github.io;

string orderTopic;
IConfiguration configuration;

configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

orderTopic = configuration.GetSection("KafkaSettings:Topics:Order").Value!;

var schemaRegistryConfig = new SchemaRegistryConfig
{
    Url = configuration.GetSection("KafkaSettings:SchemaRegistryUrl").Value
};

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = configuration.GetSection("KafkaSettings:BootstrapServer").Value,
    ClientId = configuration.GetSection("KafkaSettings:ClientId").Value,
    GroupId = configuration.GetSection("KafkaSettings:ClientId").Value,
    EnableAutoCommit = true,
    EnableAutoOffsetStore = false,
    AutoCommitIntervalMs = 500,
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var orderConsumer = new ConsumerBuilder<string, OrderEvent>(consumerConfig)
  .SetValueDeserializer(new AvroDeserializer<OrderEvent>(new CachedSchemaRegistryClient(schemaRegistryConfig)).AsSyncOverAsync())
  .Build();

orderConsumer.Subscribe(orderTopic);

var stopwatch = new Stopwatch();
var orderData = new OrderData();

var counter = 0;

stopwatch.Start();
Console.WriteLine(DateTime.Now);

while (counter != 1000)
{
    var consumeResult = orderConsumer.Consume();

    await orderData.CreateAsync(consumeResult.Message.Value);

    orderConsumer.StoreOffset(consumeResult);

    counter++;
}

stopwatch.Stop();

Console.WriteLine(stopwatch.Elapsed);
Console.WriteLine(DateTime.Now);

orderConsumer.Close();
orderConsumer.Dispose();