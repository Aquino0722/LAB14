using LabLINQ.DTOs;
using LabLINQ.Models;
using LabLINQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabLINQ.Repositories.Implementations;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }
    
    // --- Métodos de la Parte 1 ---
    public IQueryable<Product> GetProductsWithPriceGreaterThan(decimal price) => 
        Find(p => p.Price > price);
    
    public IQueryable<Product> GetProductsWithoutDescription() => 
        Find(p => string.IsNullOrEmpty(p.Description));
    
    public async Task<Product?> GetMostExpensiveProductAsync() => 
        await _context.Products.AsNoTracking().OrderByDescending(p => p.Price).FirstOrDefaultAsync();
    
    public async Task<double> GetAveragePriceAsync() => 
        await _context.Products.AverageAsync(p => (double)p.Price);

    // --- Nuevos Métodos de la Parte 2 ---
    public async Task<BestSellingProductDto?> GetBestSellingProductAsync()
    {
        return await _context.Orderdetails
            .AsNoTracking()
            .GroupBy(od => od.ProductId)
            .Select(g => new
            {
                ProductId = g.Key,
                TotalSold = g.Sum(od => od.Quantity)
            })
            .OrderByDescending(p => p.TotalSold)
            .Select(p => new BestSellingProductDto
            {
                ProductName = _context.Products.First(prod => prod.ProductId == p.ProductId).Name,
                TotalSold = p.TotalSold
            })
            .FirstOrDefaultAsync();
    }
}