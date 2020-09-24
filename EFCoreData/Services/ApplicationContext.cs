using EFCoreData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreData.Services
{
    public class ApplicationContext : DbContext
    {
        public readonly EfService Methods;
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<SportPlayer> SportPlayers { get; set; }

        public ApplicationContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
