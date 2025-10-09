using LabLINQ.Models;
using LabLINQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LabLINQ.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> expression) => 
        _context.Set<T>().Where(expression).AsNoTracking();
}