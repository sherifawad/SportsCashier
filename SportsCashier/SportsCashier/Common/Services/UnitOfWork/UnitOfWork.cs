﻿using DataBase.Models;
using DataBase.Services;
using DataBase.Services.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AppContext = DataBase.Services.AppContext;

namespace SportsCashier.Common.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _databaseContext;
        private Hashtable _repositories;
        public UnitOfWork(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        private void Dispose(bool dispose)
        {
            if (dispose)
                _databaseContext.Dispose();
        }

        public Task CommitAsync()
        {
            return _databaseContext.SaveChangesAsync();
        }

        public IRepository<int, T> Repository<T>() where T : BaseModel
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(T).Name;
            try
            {

                if (!_repositories.ContainsKey(type))
                {
                    var repositoryType = typeof(Repository<,>);
                    var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(new Type[] { typeof(int), typeof(T) }), _databaseContext);
                    _repositories.Add(type, repositoryInstance);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }

            return (IRepository<int, T>)_repositories[type];

        }
    }
}
