using LabLINQ.DTOs;
using LabLINQ.Models;

namespace LabLINQ.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    // --- Métodos de la Parte 1 ---
    IQueryable<Product> GetProductsWithPriceGreaterThan(decimal price);
    IQueryable<Product> GetProductsWithoutDescription();
    Task<Product?> GetMostExpensiveProductAsync();
    Task<double> GetAveragePriceAsync();

    // --- Nuevos Métodos de la Parte 2 ---
    Task<BestSellingProductDto?> GetBestSellingProductAsync();
}