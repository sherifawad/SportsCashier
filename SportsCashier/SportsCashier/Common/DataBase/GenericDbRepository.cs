using SportsCashier.Extensions;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SportsCashier.DataBase
{
    public class GenericDbRepository<T> : IGenericDbRepository<T> where T : IDatabaseItem, new()
    {
        readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
        });

        public SQLiteAsyncConnection Database => lazyInitializer.Value;

        public GenericDbRepository()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name))
            {
                await Database.CreateTableAsync(typeof(T)).ConfigureAwait(false);
            }
        }

        public AsyncTableQuery<T> AsQueryable() =>
            Database.Table<T>();

        public async Task<List<T>> GetItemsAsync() =>
            await AsQueryable().ToListAsync();

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = Database.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }

        public async Task<T> GetItemByIdAsync(int id) =>
            await Database.FindAsync<T>(id);

        public async Task<T> Get(Expression<Func<T, bool>> predicate) =>
             await Database.Table<T>().Where(predicate).FirstOrDefaultAsync();

        public async Task<int> SaveItemAsync(T entity, string value, string Propertyname)
        {
            if (RowExists(value, Propertyname).Result)
                return await Update(entity);
            else
                return await Insert(entity);
        }

        public async Task SaveWithChildrenAsync(T entity)
        {
            if (entity.Id != 0)
                await UpdateWithChildren(entity);
            else
                await InsertWithChildren(entity);
        }

        public async Task<int> SaveItemAsync(T entity)
        {
            if (entity.Id != 0)
                return await Update(entity);
            else
                return await Insert(entity);
        }

        public async Task<int> Insert(T entity) =>
             await Database.InsertAsync(entity);

        public async Task<int> Update(T entity) =>
             await Database.UpdateAsync(entity);

        public async Task<int> Delete(T entity) =>
             await Database.DeleteAsync(entity);

        public async Task InsertWithChildren(T element) => await Database.InsertOrReplaceWithChildrenAsync(element);
        public async Task UpdateWithChildren(T element) => await Database.UpdateWithChildrenAsync(element);

        public async Task<T> GetWithChildren(int id) => await Database.GetWithChildrenAsync<T>(id);

        public async Task<bool> RowExists(string value, string Propertyname)
        {
            bool exists = false;
            try
            {
                exists = await Task.FromResult(Database.ExecuteScalarAsync<bool>("SELECT EXISTS(SELECT 1 FROM " + typeof(T).Name + " WHERE " + Propertyname + "=?)", value).Result);
            }
            catch (Exception ex)
            {
                //Log database error
                exists = true;
            }
            return exists;
        }

        public async Task<bool> RowExists(int value, string Propertyname)
        {
            bool exists = false;
            try
            {
                exists = await Task.FromResult(Database.ExecuteScalarAsync<bool>("SELECT EXISTS(SELECT 1 FROM " + typeof(T).Name + " WHERE " + Propertyname + "=?)", value).Result);
            }
            catch (Exception ex)
            {
                //Log database error
                exists = true;
            }
            return exists;
        }


        public async Task<bool> TableMaxRows(string value)
        {
            bool exists = false;
            try
            {
                exists = await Task.FromResult(Database.ExecuteScalarAsync<bool>("DELETE FROM " + typeof(T).Name + " WHERE id <= (SELECT id FROM (SELECT id FROM " + typeof(T).Name + " ORDER BY id DESC LIMIT 1 OFFSET " + value + ") foo)").Result);
            }
            catch (Exception ex)
            {
                //Log database error
                exists = true;
            }
            return exists;
        }

    }

}
