using EFCoreData.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreData.DataAccess
{
    public interface IAppRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        void Remove(T entity);

        void Update(T entity);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        Task<int> SaveAsync();
    }
}
}
