using LabLINQ.Models;
using LabLINQ.Repositories.Interfaces;
using LabLINQ.Repositories.Implementations;

namespace LabLINQ.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IClientRepository Clients { get; private set; }
    public IProductRepository Products { get; private set; }
    public IOrderRepository Orders { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Clients = new ClientRepository(_context);
        Products = new ProductRepository(_context);
        Orders = new OrderRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}