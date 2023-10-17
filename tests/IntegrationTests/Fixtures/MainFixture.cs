using Microsoft.Extensions.Configuration;
using SampleWorker.IntegrationTests.Factories;
using SampleWorker.IntegrationTests.Fixtures.DynamoDb;
using SampleWorker.IntegrationTests.Fixtures.Kafka;

namespace SampleWorker.IntegrationTests.Fixtures;

public sealed class MainFixture : IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly CustomWebApplicationFactory _customWebApplicationFactory;
    private readonly DynamoDbFixture _dynamoDbFixture;
    private readonly KafkaFixture _kafkaFixture;

    public MainFixture()
    {
        _configuration = new ConfigurationBuilder()
          .SetBasePath(AppContext.BaseDirectory)
          .AddJsonFile("integrationtests-settings.json", optional: false)
          .Build();

        InitializeEnvironmentVariables();

        _dynamoDbFixture = new DynamoDbFixture(_configuration);

        _kafkaFixture = new KafkaFixture(Configuration);

        _customWebApplicationFactory = new CustomWebApplicationFactory();
        _ = _customWebApplicationFactory.CreateClient();
    }

    internal IConfiguration Configuration => _configuration;

    internal DynamoDbFixture DynamoDbFixture => _dynamoDbFixture;
    internal KafkaFixture KafkaFixture => _kafkaFixture;

    public void Dispose()
    {
        _dynamoDbFixture.Dispose();
        _kafkaFixture.Dispose();

        GC.SuppressFinalize(this);
    }

    private void InitializeEnvironmentVariables()
    {
        Environment.SetEnvironmentVariable("EXECUTION_ENVIRONMENT", "integration-test");
        Environment.SetEnvironmentVariable("HTTP_PROXY", "");
        Environment.SetEnvironmentVariable("HTTPS_PROXY", "");
        Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", "test");
        Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", "test");
    }
}