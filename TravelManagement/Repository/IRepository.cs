using System.Linq.Expressions;

namespace TravelManagement.Repository
{
    public interface IRepository<T> where T : class
    {

        Task CreateAsync(T Entity);
        Task RemoveAsync(T Entity);
        Task SaveAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);

    }
}

