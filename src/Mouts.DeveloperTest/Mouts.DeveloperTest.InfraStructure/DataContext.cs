using Microsoft.EntityFrameworkCore;
using Mouts.DeveloperTest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mouts.DeveloperTest.InfraStructure
{
    public sealed class DataContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Cart> Carts => Set<Cart>(); 
        public DbSet<Sale> Sales => Set<Sale>();
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
