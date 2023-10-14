namespace SampleWorker.Worker.Base;

[ExcludeFromCodeCoverage]
public class ScopedBackgroundService<T> : BackgroundService where T : IScopedBackgroundService
{
    private readonly ILogger<ScopedBackgroundService<T>> _logger;

    public ScopedBackgroundService(IServiceProvider serviceProvider, ILogger<ScopedBackgroundService<T>> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    private IServiceProvider _serviceProvider { get; }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("{Service} is stopping.", nameof(ScopedBackgroundService<T>));

        await Task.CompletedTask;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await Task.Yield();

            while (!stoppingToken.IsCancellationRequested)
            {
                await DoWorkAsync(stoppingToken);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unexpected system error has occurred. {Service} {ExceptionMessage}",
                typeof(T).Name, exception.GetBaseException().Message);
        }
    }

    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var scopedProcessingService = scope.ServiceProvider.GetRequiredService(typeof(T)) as IScopedBackgroundService;

        await scopedProcessingService!.ExecuteAsync(stoppingToken);
    }
}