using System.Linq.Expressions;
using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task EditAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> FindAllWithExpressionAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindWithExpressionAsync(Expression<Func<T,bool>> predicate);
    }
}
