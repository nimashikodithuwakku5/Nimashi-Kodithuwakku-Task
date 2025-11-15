using Microsoft.EntityFrameworkCore;
using SPILSalesOrder.Application.Interfaces;
using SPILSalesOrder.Domain.Entities;
using SPILSalesOrder.Infrastructure.Data;

namespace SPILSalesOrder.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;
    public OrderRepository(ApplicationDbContext db) => _db = db;

    public async Task AddAsync(OrderHeader order)
    {
        _db.OrderHeaders.Add(order);
        await _db.SaveChangesAsync();
    }

    public async Task<List<OrderHeader>> GetAllAsync()
    {
        return await _db.OrderHeaders
            .Include(o => o.Client)
            .Include(o => o.Details)
                .ThenInclude(d => d.Item)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<OrderHeader?> GetByIdAsync(int id)
    {
        return await _db.OrderHeaders
            .Include(o => o.Details)
                .ThenInclude(d => d.Item)
            .Include(o => o.Client)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }
}
