namespace SampleWorker.Application.UseCases.CreateOrder.DataAccess;

[ExcludeFromCodeCoverage]
internal sealed class OrderData : IOrderData
{
    private readonly IDynamoDBContext _dynamoDBContext;

    public OrderData(IDynamoDBContext dynamoDBContext)
    {
        _dynamoDBContext = dynamoDBContext;
    }

    public async Task InsertAsync(OrderEntity orderEntity)
    {
        await _dynamoDBContext.SaveAsync(orderEntity);
    }
}