using onofrej.github.io;
using SampleWorker.Application.UseCases.CreateOrder;
using SampleWorker.Worker.Base;

namespace SampleWorker.Worker.Consumers.Orders;

internal class OrderConsumer : IScopedBackgroundService
{
    private readonly int _batchSize;
    private readonly IConsumer<string, OrderEvent> _consumer;
    private readonly ICreateOrderUseCase _createOrderUseCase;
    private readonly ILogger<OrderConsumer> _logger;

    public OrderConsumer(ILogger<OrderConsumer> logger,
      IConfiguration configuration,
      IConsumer<string, OrderEvent> consumer,
      ICreateOrderUseCase createOrderUseCase)
    {
        _consumer = consumer;
        _logger = logger;
        _createOrderUseCase = createOrderUseCase;
        _batchSize = int.Parse(configuration.GetSection("kafka:Consumer:BatchSize").Value!);
    }

    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            //await Task.WhenAll(CreateTasks(stoppingToken));

            var consumeResult = _consumer.Consume(stoppingToken);

            await ProcessMessageAsync(consumeResult, stoppingToken);
        }
        catch (Exception exception)
        {
            _logger.LogConsumerError(nameof(OrderConsumer), exception);
        }

        //IEnumerable<Task> CreateTasks(CancellationToken stoppingToken)
        //{
        //    for (int counter = 0; counter < _batchSize; counter++)
        //    {
        //        var consumeResult = _consumer.Consume(stoppingToken);

        //        yield return ProcessMessageAsync(consumeResult, stoppingToken);
        //    }
        //}
    }

    private async Task ProcessMessageAsync(ConsumeResult<string, OrderEvent> consumeResult, CancellationToken stoppingToken)
    {
        try
        {
            if (consumeResult?.Message?.Value is null)
            {
                _consumer.StoreOffset(consumeResult);

                return;
            }

            var createOrderInput = new CreateOrderInput(Guid.NewGuid(),
                consumeResult.Message.Value.OrderId,
                consumeResult.Message.Value.OrderDate,
                consumeResult.Message.Value.ClientId,
                consumeResult.Message.Value.Value);

            var dateTime = DateTime.Now.ToString("dd-MM-yyyyy mm:ss:fff");

            var logInfo = new
            {
                createOrderInput.ClientId,
                createOrderInput.OrderId,
                DateTime = dateTime
            };

            _logger.LogConsumerInfo(nameof(OrderConsumer), logInfo);

            await _createOrderUseCase.ExecuteAsync(createOrderInput, stoppingToken);

            _consumer.StoreOffset(consumeResult);

            _logger.LogConsumerSuccess(nameof(OrderConsumer), logInfo);
        }
        catch (Exception exception)
        {
            _logger.LogConsumerFailure(nameof(OrderConsumer),
                consumeResult?.Message?.Value,
                exception);
        }
    }
}