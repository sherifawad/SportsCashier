using DataBase.Configuration;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text;
using static System.Environment;

namespace DataBase.Services
{
    public class AppContext : DbContext, IDatabaseContext
    {
        private readonly string _dbPath;

        public DbSet<History> Histories { get; set; }
        public DbSet<Player> Players { get; set; }
        //public DbSet<MockSportModel> sportModels { get; set; }
        public AppContext(string dbPath)
        {

            _dbPath = dbPath ?? Path.Combine(GetFolderPath(SpecialFolder.LocalApplicationData), "DataBase.db3");
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                modelBuilder.ApplyDataFixForSqlite();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            try
            {
                //optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseSqlite($"Filename={_dbPath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
