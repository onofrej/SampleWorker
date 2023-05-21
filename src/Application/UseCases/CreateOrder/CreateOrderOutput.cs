using SampleWorker.Application.Shared.ErrorHandling;

namespace SampleWorker.Application.UseCases.CreateOrder;

internal sealed class CreateOrderOutput
{
    private CreateOrderOutput()
    { }

    public Error? Error { get; private set; }

    internal static CreateOrderOutput Success()
    {
        return new CreateOrderOutput();
    }
}