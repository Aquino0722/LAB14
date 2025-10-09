using LabLINQ.DTOs;
using LabLINQ.Models;
using LabLINQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabLINQ.Repositories.Implementations;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public IQueryable<OrderDto> GetOrdersAfterDate(DateTime date)
    {
        return Find(o => o.OrderDate > date)
            .Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                ClientId = o.ClientId,
                OrderDate = o.OrderDate
            });
    }

    public async Task<int> GetTotalProductsInOrderAsync(int orderId)
    {
        return await _context.Orderdetails // Corregido a 'Orderdetails'
            .Where(od => od.OrderId == orderId)
            .SumAsync(od => od.Quantity);
    }
    
    public IQueryable<OrderDetailDto> GetOrderDetails(int orderId)
    {
        return _context.Orderdetails // Corregido a 'Orderdetails'
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
                Details = o.Orderdetails.Select(od => new OrderDetailDto // Corregido a 'Orderdetails'
                {
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity
                }).ToList()
            })
            .ToListAsync();
    }
}