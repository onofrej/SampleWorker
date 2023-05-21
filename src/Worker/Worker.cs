using SampleWorker.Application.UseCases.CreateOrder;

namespace SampleWorker.Worker;

public class Worker : IHostedService
{
    private readonly ILogger<Worker> _logger;
    private readonly ICreateOrderUseCase _createOrderUseCase;

    public Worker(ILogger<Worker> logger, ICreateOrderUseCase createOrderUseCase)
    {
        _createOrderUseCase = createOrderUseCase;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await ExecuteAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("{Type} Worker stopped", nameof(Worker));

        return Task.CompletedTask;
    }

    private async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _createOrderUseCase.ExecuteAsync(new CreateOrderInput(Guid.NewGuid()), stoppingToken);

            await Task.Delay(100, stoppingToken);
        }
    }
}