namespace SampleWorker.Application.UseCases.CreateOrder
{
    public interface ICreateOrderUseCase
    {
        Task ExecuteAsync(CreateOrderInput input, CancellationToken cancellationToken);
    }
}