using SPILSalesOrder.Domain.Entities;

namespace SPILSalesOrder.Application.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task AddAsync(Client client);
    }
}
