using SPILSalesOrder.Application.Interfaces;
using SPILSalesOrder.Domain.Entities;

namespace SPILSalesOrder.Application.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Task<List<Client>> GetAllAsync() => _clientRepository.GetAllAsync();

        public Task<Client?> GetByIdAsync(int id) => _clientRepository.GetByIdAsync(id);
    }
}
