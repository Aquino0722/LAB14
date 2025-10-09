using LabLINQ.DTOs;
using LabLINQ.Models;

namespace LabLINQ.Repositories.Interfaces;

public interface IClientRepository : IRepository<Client>
{
    Task<ClientOrderCountDto?> GetClientWithMostOrdersAsync();
    Task<List<string>> GetProductsSoldToClientAsync(int clientId);
    Task<List<string>> GetClientsWhoBoughtProductAsync(int productId);
}