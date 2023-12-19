namespace SampleWorker.Application.UseCases.CreateOrder;

[ExcludeFromCodeCoverage]
public record CreateOrderInput(Guid Id,
    string OrderId,
    string OrderDate,
    string ClientId,
    double Value);