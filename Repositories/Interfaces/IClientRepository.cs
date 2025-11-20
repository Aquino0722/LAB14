using LabLINQ.DTOs;
using LabLINQ.Models;

namespace LabLINQ.Repositories.Interfaces;

public interface IClientRepository : IRepository<Client>
{
    // --- Métodos de la Parte 1 ---
    Task<ClientOrderCountDto?> GetClientWithMostOrdersAsync();
    Task<List<string>> GetProductsSoldToClientAsync(int clientId);
    Task<List<string>> GetClientsWhoBoughtProductAsync(int productId);

    // --- Nuevos Métodos de la Parte 2 ---
    Task<List<ClientOrderDto>> GetClientOrdersAsync();
    Task<List<ClientProductCountDto>> GetClientsWithProductCountAsync();
    Task<List<ClientDto>> GetInactiveClientsSinceAsync(DateTime date);
}