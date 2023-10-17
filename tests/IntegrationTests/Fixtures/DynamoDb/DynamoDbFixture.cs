using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Configuration;
using SampleWorker.Application.UseCases.CreateOrder.DataAccess;

namespace SampleWorker.IntegrationTests.Fixtures.DynamoDb;

internal sealed class DynamoDbFixture : IDisposable
{
    private readonly AmazonDynamoDBClient _amazonDynamoDBClient;

    private readonly IConfiguration _configuration;

    private readonly List<Table> _tables = new()
    {
        new Table(OrderEntity.TableName, OrderEntity.HashKeyName, OrderEntity.SortKeyName)
    };

    public DynamoDbFixture(IConfiguration configuration)
    {
        _configuration = configuration;

        _amazonDynamoDBClient = new AmazonDynamoDBClient(new AmazonDynamoDBConfig
        {
            ServiceURL = _configuration.GetSection("AWSLocalstackSettings:ServiceURL").Value
        });

        CreateTablesAsync().Wait();
    }

    public void Dispose()
    {
        DeleteTablesAsync().Wait();
    }

    public async Task InsertAsync<T>(T entity)
    {
        var dynamoDBContext = new DynamoDBContext(_amazonDynamoDBClient);

        await dynamoDBContext.SaveAsync(entity);
    }

    public async Task<T?> ReadAsync<T>(string hashKey, string? hashKeyValue)
    {
        var dynamoDBContext = new DynamoDBContext(_amazonDynamoDBClient);

        var collections = await dynamoDBContext.FromQueryAsync<T>(new QueryOperationConfig
        {
            Filter = new QueryFilter(hashKey, QueryOperator.Equal, hashKeyValue)
        }).GetRemainingAsync();

        return collections.FirstOrDefault();
    }

    private async Task CreateTablesAsync()
    {
        foreach (var table in _tables)
        {
            var attributeDefinitions = new List<AttributeDefinition>()
            {
                new AttributeDefinition
                {
                    AttributeName = table.HashKeyName,
                    AttributeType = ScalarAttributeType.S
                }
            };

            var keySchemas = new List<KeySchemaElement>()
            {
                new KeySchemaElement
                {
                    AttributeName = table.HashKeyName,
                    KeyType = KeyType.HASH
                }
            };

            if (!string.IsNullOrWhiteSpace(table.SortKeyName))
            {
                attributeDefinitions.Add(new AttributeDefinition
                {
                    AttributeName = table.SortKeyName,
                    AttributeType = ScalarAttributeType.S
                });

                keySchemas.Add(new KeySchemaElement
                {
                    AttributeName = table.SortKeyName,
                    KeyType = KeyType.RANGE
                });
            }

            var request = new CreateTableRequest
            {
                TableName = table.Name,
                AttributeDefinitions = attributeDefinitions,
                KeySchema = keySchemas,
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                }
            };

            await _amazonDynamoDBClient.CreateTableAsync(request);
        }
    }

    private async Task DeleteTablesAsync()
    {
        foreach (var table in _tables)
        {
            await _amazonDynamoDBClient.DeleteTableAsync(new DeleteTableRequest { TableName = table.Name });
        }
    }
}

internal record Table(string? Name, string? HashKeyName, string? SortKeyName);