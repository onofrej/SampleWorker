using SampleWorker.Application.UseCases.CreateOrder.DataAccess;

namespace SampleWorker.Application.UseCases.CreateOrder;

[ExcludeFromCodeCoverage]
internal sealed class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly ILogger<CreateOrderUseCase> _logger;
    private readonly IOrderData _orderData;

    public CreateOrderUseCase(ILogger<CreateOrderUseCase> logger, IOrderData orderData)
    {
        _logger = logger;
        _orderData = orderData;
    }

    public async Task ExecuteAsync(CreateOrderInput input, CancellationToken cancellationToken)
    {
        await _orderData.InsertAsync(new OrderEntity(input.ClientId,
            input.OrderDate,
            input.OrderId,
            input.Value));

        _logger.LogUseCaseSuccess(nameof(CreateOrderUseCase), input.Id);
    }
}