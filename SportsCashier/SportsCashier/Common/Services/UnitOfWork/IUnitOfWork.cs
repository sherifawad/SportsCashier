using DataBase.Models;
using DataBase.Services.Repository;
using System;
using System.Threading.Tasks;

namespace SportsCashier.Common.Services.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        IRepository<int, T> Repository<T>() where T : BaseModel;
    }
}