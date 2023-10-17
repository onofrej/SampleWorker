using Amazon.DynamoDBv2.DataModel;

namespace SampleWorker.Application.UseCases.CreateOrder.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
public class OrderEntity
{
    public const string HashKeyName = "cod_idt_nota_crrg";
    public const string SortKeyName = "cod_idt_invt";
    public const string TableName = "tbfd9144_nota_rend_vrvl";

    [DynamoDBHashKey(HashKeyName)]
    public string? BrokerNoteId { get; set; }

    [DynamoDBProperty("num_nota_crrg")]
    public string? BrokerNoteNumber { get; set; }

    [DynamoDBProperty("cod_idt_clie")]
    public string? ClientId { get; set; }

    [DynamoDBProperty("numero_nota_investidor_interno")]
    public string? InternalBrokerNoteId { get; set; }

    [DynamoDBProperty("cod_idt_fami_prod")]
    public string? InternalCode { get; set; }

    [DynamoDBRangeKey(SortKeyName)]
    public string? InvestmentId { get; set; }
}