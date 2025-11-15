using SPILSalesOrder.Application.Interfaces;
using SPILSalesOrder.Domain.Entities;

namespace SPILSalesOrder.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IItemRepository _itemRepository;

        public OrderService(IOrderRepository orderRepository, IClientRepository clientRepository, IItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _itemRepository = itemRepository;
        }

        public Task<List<OrderHeader>> GetAllAsync() => _orderRepository.GetAllAsync();

        public Task<OrderHeader?> GetByIdAsync(int id) => _orderRepository.GetByIdAsync(id);

        public async Task AddAsync(OrderHeader order)
        {
            // Server-side validation: ensure Client exists
            var client = await _clientRepository.GetByIdAsync(order.ClientId);
            if (client == null)
                throw new ArgumentException($"Client with id {order.ClientId} does not exist.");

            // Validate items referenced by details
            var missingItems = new List<int>();
            foreach (var d in order.Details)
            {
                if (d.ItemId <= 0)
                {
                    missingItems.Add(d.ItemId);
                    continue;
                }
                var item = await _itemRepository.GetByIdAsync(d.ItemId);
                if (item == null)
                    missingItems.Add(d.ItemId);
            }
            if (missingItems.Any())
                throw new ArgumentException($"The following ItemId(s) do not exist: {string.Join(',', missingItems.Distinct())}");

            // Basic server-side calculation: set amounts if missing
            foreach (var d in order.Details)
            {
                // prefer line Price if provided, otherwise use linked Item price
                var unitPrice = d.Price ?? d.Item?.Price ?? 0m;
                if (!d.ExclAmount.HasValue)
                    d.ExclAmount = Math.Round(d.Quantity * unitPrice, 2);
                if (!d.TaxAmount.HasValue && d.TaxRate.HasValue)
                    d.TaxAmount = Math.Round((d.ExclAmount ?? 0) * (d.TaxRate.Value / 100m), 2);
                if (!d.InclAmount.HasValue)
                    d.InclAmount = Math.Round((d.ExclAmount ?? 0) + (d.TaxAmount ?? 0), 2);
            }

            await _orderRepository.AddAsync(order);
        }
    }
}
