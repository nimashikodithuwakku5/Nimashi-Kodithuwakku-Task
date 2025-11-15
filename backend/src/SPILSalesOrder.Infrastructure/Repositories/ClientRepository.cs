using Microsoft.EntityFrameworkCore;
using SPILSalesOrder.Application.Interfaces;
using SPILSalesOrder.Domain.Entities;
using SPILSalesOrder.Infrastructure.Data;

namespace SPILSalesOrder.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _db;
    public ClientRepository(ApplicationDbContext db) => _db = db;

    public async Task AddAsync(Client client)
    {
        _db.Clients.Add(client);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Client>> GetAllAsync()
    {
        return await _db.Clients.AsNoTracking().ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _db.Clients.FindAsync(id);
    }
}
