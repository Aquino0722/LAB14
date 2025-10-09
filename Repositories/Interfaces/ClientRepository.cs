using LabLINQ.DTOs;
using LabLINQ.Models;
using LabLINQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabLINQ.Repositories.Implementations;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context) { }

    public async Task<ClientOrderCountDto?> GetClientWithMostOrdersAsync()
    {
        return await _context.Clients
            .AsNoTracking()
            .Select(c => new
            {
                ClientName = c.Name,
                OrderCount = c.Orders.Count()
            })
            .OrderByDescending(x => x.OrderCount)
            .Select(x => new ClientOrderCountDto { ClientName = x.ClientName, OrderCount = x.OrderCount })
            .FirstOrDefaultAsync();
    }
    
    public async Task<List<string>> GetProductsSoldToClientAsync(int clientId)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.ClientId == clientId)
            .SelectMany(o => o.Orderdetails) // Corregido a 'Orderdetails'
            .Select(od => od.Product.Name)
            .Distinct()
            .ToListAsync();
    }
    
    public async Task<List<string>> GetClientsWhoBoughtProductAsync(int productId)
    {
        return await _context.Orderdetails // Corregido a 'Orderdetails'
            .AsNoTracking()
            .Where(od => od.ProductId == productId)
            .Select(od => od.Order.Client.Name)
            .Distinct()
            .ToListAsync();
    }
}