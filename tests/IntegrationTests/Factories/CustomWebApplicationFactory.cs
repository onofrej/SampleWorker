namespace SampleWorker.IntegrationTests.Factories;

internal sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTest");

        return base.CreateHost(builder);
    }
}