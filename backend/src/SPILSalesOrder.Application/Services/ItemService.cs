using SPILSalesOrder.Application.Interfaces;
using SPILSalesOrder.Domain.Entities;

namespace SPILSalesOrder.Application.Services
{
    public class ItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public Task<List<Item>> GetAllAsync() => _itemRepository.GetAllAsync();

        public Task<Item?> GetByIdAsync(int id) => _itemRepository.GetByIdAsync(id);
    }
}
