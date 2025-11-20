using LabLINQ.DTOs;
using LabLINQ.Models;
using LabLINQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabLINQ.Repositories.Implementations;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    // ... (Aquí van todos los métodos de la Parte 1, sin cambios) ...
    public IQueryable<DTOs.OrderDto> GetOrdersAfterDate(DateTime date)
    {
        return Find(o => o.OrderDate > date)
            .Select(o => new DTOs.OrderDto
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate
            });
    }

    public async Task<int> GetTotalProductsInOrderAsync(int orderId)
    {
        return await _context.Orderdetails
            .Where(od => od.OrderId == orderId)
            .SumAsync(od => od.Quantity);
    }

    public IQueryable<OrderDetailDto> GetOrderDetails(int orderId)
    {
        return _context.Orderdetails
            .AsNoTracking()
            .Where(od => od.OrderId == orderId)
            .Select(od => new OrderDetailDto
            {
                ProductName = od.Product.Name,
                Quantity = od.Quantity
            });
    }

    public async Task<List<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync()
    {
        return await _context.Orders
            .AsNoTracking()
            .Select(o => new OrderWithDetailsDto
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                Details = o.Orderdetails.Select(od => new OrderDetailDto
                {
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity
                }).ToList()
            })
            .ToListAsync();
    }


    // --- Nuevos Métodos de la Parte 2 (con la corrección) ---
    public async Task<List<OrderDetailsDto>> GetOrdersWithProductDetailsAsync()
    {
        return await _context.Orders
            .Include(order => order.Orderdetails)
            .ThenInclude(orderDetail => orderDetail.Product)
            .AsNoTracking()
            .Select(order => new OrderDetailsDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                // CORRECCIÓN: Ahora mapeamos al nuevo DTO específico
                Products = order.Orderdetails.Select(od => new OrderDetailProductDto
                {
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity,
                    Price = od.Product.Price
                }).ToList()
            }).ToListAsync();
    }

    public async Task<List<SalesByClientDto>> GetSalesByClientAsync()
    {
        return await _context.Orders
            .AsNoTracking()
            .GroupBy(order => order.Client)
            .Select(group => new SalesByClientDto
            {
                ClientName = group.Key.Name,
                TotalSales = group.SelectMany(o => o.Orderdetails)
                                    .Sum(od => od.Quantity * od.Product.Price)
            })
            .OrderByDescending(s => s.TotalSales)
            .ToListAsync();
    }

    public async Task<List<MonthlySalesDto>> GetMonthlySalesAsync()
    {
        return await _context.Orderdetails
            .AsNoTracking()
            .GroupBy(od => new { od.Order.OrderDate.Year, od.Order.OrderDate.Month })
            .Select(g => new MonthlySalesDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalSales = g.Sum(od => od.Quantity * od.Product.Price)
            })
            .OrderBy(s => s.Year)
            .ThenBy(s => s.Month)
            .ToListAsync();
    }
}