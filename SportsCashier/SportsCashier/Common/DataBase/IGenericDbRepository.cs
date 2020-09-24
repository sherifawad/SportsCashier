using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SportsCashier.DataBase
{
    public interface IGenericDbRepository<T> where T : IDatabaseItem, new()
    {
        AsyncTableQuery<T> AsQueryable();
        Task<int> Delete(T entity);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        Task<T> GetItemByIdAsync(int id);

        Task<List<T>> GetItemsAsync();
        Task<int> Insert(T entity);
        Task<int> SaveItemAsync(T entity, string value, string Propertyname);

        Task SaveWithChildrenAsync(T entity);
        Task<int> SaveItemAsync(T entity);
        Task<int> Update(T entity);

        Task InsertWithChildren(T element);
        Task UpdateWithChildren(T element);
        Task<T> GetWithChildren(int id);

        Task<bool> RowExists(string value, string Propertyname);
        Task<bool> RowExists(int value, string Propertyname);

    }
}
