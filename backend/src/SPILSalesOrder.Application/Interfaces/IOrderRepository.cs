using SPILSalesOrder.Domain.Entities;

namespace SPILSalesOrder.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<OrderHeader>> GetAllAsync();
        Task<OrderHeader?> GetByIdAsync(int id);
        Task AddAsync(OrderHeader order);
    }
}
