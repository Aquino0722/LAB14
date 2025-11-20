using LabLINQ.DTOs;
using LabLINQ.Models;
using LabLINQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabLINQ.Repositories.Implementations;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context) { }

    // --- Métodos de la Parte 1 ---
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
            .SelectMany(o => o.Orderdetails)
            .Select(od => od.Product.Name)
            .Distinct()
            .ToListAsync();
    }
    
    public async Task<List<string>> GetClientsWhoBoughtProductAsync(int productId)
    {
        return await _context.Orderdetails
            .AsNoTracking()
            .Where(od => od.ProductId == productId)
            .Select(od => od.Order.Client.Name)
            .Distinct()
            .ToListAsync();
    }
    
    // --- Nuevos Métodos de la Parte 2 ---
    public async Task<List<ClientOrderDto>> GetClientOrdersAsync()
    {
        return await _context.Clients
            .AsNoTracking()
            .Select(client => new ClientOrderDto
            {
                ClientName = client.Name,
                Orders = client.Orders.Select(order => new DTOs.OrderDto
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate
                }).ToList()
            }).ToListAsync();
    }

    public async Task<List<ClientProductCountDto>> GetClientsWithProductCountAsync()
    {
        return await _context.Clients
            .AsNoTracking()
            .Select(client => new ClientProductCountDto
            {
                ClientName = client.Name,
                TotalProducts = client.Orders.SelectMany(o => o.Orderdetails).Sum(od => od.Quantity)
            }).ToListAsync();
    }

    public async Task<List<ClientDto>> GetInactiveClientsSinceAsync(DateTime date)
    {
        return await _context.Clients
            .AsNoTracking()
            .Where(c => c.Orders.Any() && c.Orders.All(o => o.OrderDate < date))
            .Select(c => new ClientDto
            {
                ClientId = c.ClientId,
                Name = c.Name,
                Email = c.Email
            })
            .ToListAsync();
    }
}