//using Worker;

//IHost host = Host.CreateDefaultBuilder(args)
//    .ConfigureServices(services =>
//    {
//        services.AddHostedService<Worker>();
//    })
//    .Build();

//await host.RunAsync();

using Microsoft.AspNetCore.Builder;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(Log.Logger);

    builder.Services.AddHealthChecks();

    var app = builder.Build();

    app.UseHealthChecks("/health");
    app.UseRouting();
    app.Run();
}
catch (Exception exception)
{
    Log.Logger.Fatal(exception.ToString());
}