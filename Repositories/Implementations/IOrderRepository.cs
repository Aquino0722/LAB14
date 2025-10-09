using LabLINQ.DTOs;
using LabLINQ.Models;

namespace LabLINQ.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    IQueryable<OrderDto> GetOrdersAfterDate(DateTime date);
    Task<int> GetTotalProductsInOrderAsync(int orderId);
    IQueryable<OrderDetailDto> GetOrderDetails(int orderId);
    Task<List<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync();
}