using System.Linq.Expressions;

namespace LabLINQ.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> Find(Expression<Func<T, bool>> expression);
}