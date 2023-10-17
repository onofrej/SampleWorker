namespace SampleWorker.IntegrationTests.Tests;

public abstract class BaseIntegratedTest
{
    protected static CancellationToken GetCancellationToken
    {
        get
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(600_000);
            return cancellationTokenSource.Token;
        }
    }

    protected virtual async Task<TResult> ExecutePolicyAsync<TResult>(Func<TResult, bool> predicate,
        int numberOfRetries,
        double sleepDurantionProviderInSeconds,
        Func<Task<TResult>> action)
    {
        return await Policy.HandleResult(predicate)
            .WaitAndRetryAsync(numberOfRetries, sleep => TimeSpan.FromSeconds(sleepDurantionProviderInSeconds))
            .ExecuteAsync(action);
    }
}