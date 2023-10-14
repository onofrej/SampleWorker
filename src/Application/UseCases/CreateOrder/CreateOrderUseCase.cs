namespace SampleWorker.Application.UseCases.CreateOrder;

[ExcludeFromCodeCoverage]
internal sealed class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly ILogger<CreateOrderUseCase> _logger;

    public CreateOrderUseCase(ILogger<CreateOrderUseCase> logger)
    {
        _logger = logger;
    }

    public async Task ExecuteAsync(CreateOrderInput input, CancellationToken cancellationToken)
    {
        _logger.LogUseCaseSuccess(nameof(CreateOrderUseCase), input.Id);
    }
}