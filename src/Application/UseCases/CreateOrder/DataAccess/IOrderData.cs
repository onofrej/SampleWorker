namespace SampleWorker.Application.UseCases.CreateOrder.DataAccess
{
    public interface IOrderData
    {
        Task InsertAsync(OrderEntity orderEntity);
    }
}