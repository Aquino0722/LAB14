using LabLINQ.Models;

namespace LabLINQ.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    IQueryable<Product> GetProductsWithPriceGreaterThan(decimal price);
    IQueryable<Product> GetProductsWithoutDescription();
    Task<Product?> GetMostExpensiveProductAsync();
    Task<double> GetAveragePriceAsync();
}