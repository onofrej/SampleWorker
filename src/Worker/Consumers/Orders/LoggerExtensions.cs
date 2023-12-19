namespace SampleWorker.Worker.Consumers.Orders;

[ExcludeFromCodeCoverage]
internal static class LoggerExtensions
{
    private const string ConsumerErrorMessage = "[{Type}] An unexpected error has occured: {exception}";
    private const string ConsumerFailureMessage = "[{Type}] The message was processed with failure: {exception} - {logInfo}";
    private const string ConsumerInfoMessage = "[{Type}] The message was received with: {logInfo}";
    private const string ConsumerSuccessMessage = "[{Type}] The message was processed successfully with: {logInfo}";

    private static Action<ILogger, string, string, Exception> ConsumerError =>
      LoggerMessage.Define<string, string>(LogLevel.Error, new EventId((int)LogLevel.Error, nameof(LogConsumerError)), ConsumerErrorMessage);

    private static Action<ILogger, string, string, object?, Exception> ConsumerFailure =>
      LoggerMessage.Define<string, string, object?>(LogLevel.Error, new EventId((int)LogLevel.Error, nameof(LogConsumerError)), ConsumerFailureMessage);

    private static Action<ILogger, string, object, Exception?> ConsumerInfo =>
      LoggerMessage.Define<string, object>(LogLevel.Information, new EventId((int)LogLevel.Information, nameof(LogConsumerInfo)), ConsumerInfoMessage);

    private static Action<ILogger, string, object, Exception?> ConsumerSuccess =>
      LoggerMessage.Define<string, object>(LogLevel.Information, new EventId((int)LogLevel.Information, nameof(LogConsumerSuccess)), ConsumerSuccessMessage);

    public static void LogConsumerError(this ILogger logger, string type, Exception exception)
    {
        ConsumerError(logger, type, exception.Message, exception);
    }

    public static void LogConsumerFailure(this ILogger logger, string type, object? logInfo, Exception exception)
    {
        ConsumerFailure(logger, type, exception.Message, logInfo, exception);
    }

    public static void LogConsumerInfo(this ILogger logger, string type, object logInfo)
    {
        ConsumerInfo(logger, type, logInfo, default);
    }

    public static void LogConsumerSuccess(this ILogger logger, string type, object logInfo)
    {
        ConsumerSuccess(logger, type, logInfo, default);
    }
}