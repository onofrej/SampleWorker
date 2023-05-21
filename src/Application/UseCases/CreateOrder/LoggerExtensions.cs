namespace SampleWorker.Application.UseCases.CreateOrder;

[ExcludeFromCodeCoverage]
internal static class LoggerExtensions
{
    private const string UseCaseSuccessMessage = "[{Type}] was successfully processed. CorrelationId: {CorrelationId}";

    private static Action<ILogger, string, Guid, Exception> UseCaseSuccess =>
        LoggerMessage.Define<string, Guid>(LogLevel.Information, new EventId((int)LogLevel.Information), UseCaseSuccessMessage);

    public static void LogUseCaseSuccess(this ILogger logger, string type, Guid correlationId)
    {
        UseCaseSuccess(logger, type, correlationId, null);
    }
}