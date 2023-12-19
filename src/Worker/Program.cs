using SampleWorker.Worker.DependencyInjection;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    Log.Logger.Information("Application started in environment {EnvironmentName}", builder.Environment.EnvironmentName);

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(Log.Logger);

    builder.Services.AddHealthChecks();
    builder.Services.InitializeAppliactionServices(builder.Configuration);

    var app = builder.Build();

    app.UseHealthChecks("/health");
    app.UseRouting();
    app.Run();
}
catch (Exception exception)
{
    Log.Logger.Fatal(exception.ToString());
}

[ExcludeFromCodeCoverage]
public partial class Program
{
    protected Program()
    { }
}