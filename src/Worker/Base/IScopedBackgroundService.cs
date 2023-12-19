namespace SampleWorker.Worker.Base
{
    public interface IScopedBackgroundService
    {
        Task ExecuteAsync(CancellationToken stoppingToken);
    }
}