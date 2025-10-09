using LabLINQ.Repositories.Interfaces;

namespace LabLINQ.Repositories;

public interface IUnitOfWork : IDisposable
{
    IClientRepository Clients { get; }
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    Task<int> CompleteAsync();
}