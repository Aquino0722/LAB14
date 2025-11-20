using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabLINQ.DTOs;
using LabLINQ.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabLINQ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ExercisesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // --- Endpoints de la Parte 1 (Ejercicios 1-12) ---

    // GET api/exercises/1/clients-by-name?name=Juan
    [HttpGet("1/clients-by-name")]
    public async Task<IActionResult> GetClientsByName([FromQuery] string name)
    {
        var clientsQuery = _unitOfWork.Clients.Find(c => c.Name.StartsWith(name));
        var clientDtos = await clientsQuery
            .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(clientDtos);
    }

    // GET api/exercises/2/products-pricier-than/20
    [HttpGet("2/products-pricier-than/{price}")]
    public async Task<IActionResult> GetProductsPricierThan(decimal price)
    {
        var productsQuery = _unitOfWork.Products.GetProductsWithPriceGreaterThan(price);
        var productDtos = await productsQuery
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(productDtos);
    }

    // GET api/exercises/3/order-product-details/1
    [HttpGet("3/order-product-details/{orderId}")]
    public async Task<IActionResult> GetOrderProductDetails(int orderId)
    {
        var detailDtos = await _unitOfWork.Orders.GetOrderDetails(orderId).ToListAsync();
        return Ok(detailDtos);
    }

    // GET api/exercises/4/total-products-in-order/1
    [HttpGet("4/total-products-in-order/{orderId}")]
    public async Task<IActionResult> GetTotalProductsInOrder(int orderId)
    {
        var total = await _unitOfWork.Orders.GetTotalProductsInOrderAsync(orderId);
        return Ok(new { orderId, totalQuantity = total });
    }

    // GET api/exercises/5/most-expensive-product
    [HttpGet("5/most-expensive-product")]
    public async Task<IActionResult> GetMostExpensiveProduct()
    {
        var product = await _unitOfWork.Products.GetMostExpensiveProductAsync();
        return Ok(_mapper.Map<ProductDto>(product));
    }

    // GET api/exercises/6/orders-after-date/2025-05-01
    [HttpGet("6/orders-after-date/{date}")]
    public async Task<IActionResult> GetOrdersAfterDate(DateTime date)
    {
        var orderDtos = await _unitOfWork.Orders.GetOrdersAfterDate(date).ToListAsync();
        return Ok(orderDtos);
    }
    
    // GET api/exercises/7/average-product-price
    [HttpGet("7/average-product-price")]
    public async Task<IActionResult> GetAverageProductPrice()
    {
        var avgPrice = await _unitOfWork.Products.GetAveragePriceAsync();
        return Ok(new { averagePrice = avgPrice });
    }

    // GET api/exercises/8/products-without-description
    [HttpGet("8/products-without-description")]
    public async Task<IActionResult> GetProductsWithoutDescription()
    {
        var productsQuery = _unitOfWork.Products.GetProductsWithoutDescription();
        var productDtos = await productsQuery
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(productDtos);
    }
    
    // GET api/exercises/9/client-with-most-orders
    [HttpGet("9/client-with-most-orders")]
    public async Task<IActionResult> GetClientWithMostOrders()
    {
        var client = await _unitOfWork.Clients.GetClientWithMostOrdersAsync();
        return Ok(client);
    }

    // GET api/exercises/10/orders-with-details
    [HttpGet("10/orders-with-details")]
    public async Task<IActionResult> GetAllOrdersWithDetails()
    {
        var orders = await _unitOfWork.Orders.GetAllOrdersWithDetailsAsync();
        return Ok(orders);
    }

    // GET api/exercises/11/products-sold-to-client/1
    [HttpGet("11/products-sold-to-client/{clientId}")]
    public async Task<IActionResult> GetProductsSoldToClient(int clientId)
    {
        var products = await _unitOfWork.Clients.GetProductsSoldToClientAsync(clientId);
        return Ok(products);
    }

    // GET api/exercises/12/clients-who-bought-product/2
    [HttpGet("12/clients-who-bought-product/{productId}")]
    public async Task<IActionResult> GetClientsWhoBoughtProduct(int productId)
    {
        var clients = await _unitOfWork.Clients.GetClientsWhoBoughtProductAsync(productId);
        return Ok(clients);
    }

    // --- Endpoints de la Parte 2 (Ejercicios 13-19) ---

    // GET api/exercises/13/client-orders
    [HttpGet("13/client-orders")]
    public async Task<IActionResult> GetClientOrders()
    {
        var result = await _unitOfWork.Clients.GetClientOrdersAsync();
        return Ok(result);
    }

    // GET api/exercises/14/orders-with-product-details
    [HttpGet("14/orders-with-product-details")]
    public async Task<IActionResult> GetOrdersWithProductDetails()
    {
        var result = await _unitOfWork.Orders.GetOrdersWithProductDetailsAsync();
        return Ok(result);
    }
    
    // GET api/exercises/15/clients-product-count
    [HttpGet("15/clients-product-count")]
    public async Task<IActionResult> GetClientsWithProductCount()
    {
        var result = await _unitOfWork.Clients.GetClientsWithProductCountAsync();
        return Ok(result);
    }
    
    // GET api/exercises/16/sales-by-client
    [HttpGet("16/sales-by-client")]
    public async Task<IActionResult> GetSalesByClient()
    {
        var result = await _unitOfWork.Orders.GetSalesByClientAsync();
        return Ok(result);
    }
    
    // GET api/exercises/17/best-selling-product
    [HttpGet("17/best-selling-product")]
    public async Task<IActionResult> GetBestSellingProduct()
    {
        var product = await _unitOfWork.Products.GetBestSellingProductAsync();
        return Ok(product);
    }
    
    // GET api/exercises/18/inactive-clients-since/2025-05-03
    [HttpGet("18/inactive-clients-since/{date}")]
    public async Task<IActionResult> GetInactiveClientsSince(DateTime date)
    {
        var clients = await _unitOfWork.Clients.GetInactiveClientsSinceAsync(date);
        return Ok(clients);
    }
    
    // GET api/exercises/19/monthly-sales
    [HttpGet("19/monthly-sales")]
    public async Task<IActionResult> GetMonthlySales()
    {
        var sales = await _unitOfWork.Orders.GetMonthlySalesAsync();
        return Ok(sales);
    }
}