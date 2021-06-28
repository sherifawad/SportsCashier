using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using static System.Environment;

namespace DataBase.Services
{
    public class AppContext : DbContext, IDatabaseContext
    {
        public AppContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.EnableSensitiveDataLogging();
            var dbPath = Path.Combine(GetFolderPath(SpecialFolder.LocalApplicationData), "DataBase.db3");
            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
