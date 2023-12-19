using onofrej.github.io;
using SampleWorker.Application.UseCases.CreateOrder.DataAccess;

namespace SampleWorker.IntegrationTests.Tests;

[Collection("Test collection")]
public class OrderConsumerTests : BaseIntegratedTest
{
    private const int NumberOfRetries = 10;
    private const int SleepDurantionProviderInSeconds = 2;
    private readonly IFixture _fixture = new Fixture();
    private readonly MainFixture _mainFixture;

    public OrderConsumerTests(MainFixture mainFixture) => _mainFixture = mainFixture;

    [Fact(DisplayName = "Message is consumed and order does not exist in the database then order is inserted in the database")]
    public async Task MessageConsumedAndOrderNotExistInDatabaseThenOrderIsInserted()
    {
        //Arrange
        var message = _fixture.Create<OrderEvent>();
        var orderId = message.OrderId;

        //Act
        _mainFixture.KafkaFixture.ProduceBrokeNotesMessage(message);

        //Assert
        var orderEntity = await base.ExecutePolicyAsync(entity => entity == null,
            NumberOfRetries, SleepDurantionProviderInSeconds, () =>
                _mainFixture.DynamoDbFixture.ReadAsync<OrderEntity>(OrderEntity.HashKeyName, orderId));

        orderEntity.Should().NotBeNull();
        orderEntity?.OrderId.Should().Be(orderId);
    }
}