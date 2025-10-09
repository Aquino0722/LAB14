using LabLINQ.Models;
using LabLINQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabLINQ.Repositories.Implementations;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }
    
    public IQueryable<Product> GetProductsWithPriceGreaterThan(decimal price) => 
        Find(p => p.Price > price);
    
    public IQueryable<Product> GetProductsWithoutDescription() => 
        Find(p => string.IsNullOrEmpty(p.Description));
    
    public async Task<Product?> GetMostExpensiveProductAsync() => 
        await _context.Products.AsNoTracking().OrderByDescending(p => p.Price).FirstOrDefaultAsync();
    
    public async Task<double> GetAveragePriceAsync() => 
        await _context.Products.AverageAsync(p => (double)p.Price);
}