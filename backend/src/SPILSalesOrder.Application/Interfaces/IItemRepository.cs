using SPILSalesOrder.Domain.Entities;

namespace SPILSalesOrder.Application.Interfaces
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync();
        Task<Item?> GetByIdAsync(int id);
    }
}
