using Microsoft.EntityFrameworkCore;
using SPILSalesOrder.Application.Interfaces;
using SPILSalesOrder.Domain.Entities;
using SPILSalesOrder.Infrastructure.Data;

namespace SPILSalesOrder.Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _db;
    public ItemRepository(ApplicationDbContext db) => _db = db;

    public async Task<List<Item>> GetAllAsync()
    {
        return await _db.Items.AsNoTracking().ToListAsync();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _db.Items.FindAsync(id);
    }
}
