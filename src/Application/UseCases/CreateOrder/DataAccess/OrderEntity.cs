namespace SampleWorker.Application.UseCases.CreateOrder.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
public class OrderEntity
{
    public const string HashKeyName = "order_id";
    public const string SortKeyName = "client_id";
    public const string TableName = "order-table";

    public OrderEntity()
    {
    }

    public OrderEntity(string? clientId, string? orderDate, string? orderId, double value)
    {
        ClientId = clientId;
        OrderDate = orderDate;
        OrderId = orderId;
        Value = value;
    }

    [DynamoDBRangeKey(SortKeyName)]
    public string? ClientId { get; private set; }

    [DynamoDBProperty("order_date")]
    public string? OrderDate { get; private set; }

    [DynamoDBHashKey(HashKeyName)]
    public string? OrderId { get; private set; }

    [DynamoDBProperty("value")]
    public double Value { get; private set; }
}