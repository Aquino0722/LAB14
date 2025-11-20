using LabLINQ.DTOs;
using LabLINQ.Models;

namespace LabLINQ.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    // --- Métodos de la Parte 1 ---
    IQueryable<DTOs.OrderDto> GetOrdersAfterDate(DateTime date); // Especificamos el DTO correcto
    Task<int> GetTotalProductsInOrderAsync(int orderId);
    IQueryable<OrderDetailDto> GetOrderDetails(int orderId);
    Task<List<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync();

    // --- Nuevos Métodos de la Parte 2 ---
    Task<List<OrderDetailsDto>> GetOrdersWithProductDetailsAsync();
    Task<List<SalesByClientDto>> GetSalesByClientAsync();
    Task<List<MonthlySalesDto>> GetMonthlySalesAsync();
}