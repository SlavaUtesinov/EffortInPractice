using Context.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbConnection connection) : base(connection, true)
        {            
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryBalance> CategoryBalances { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            OnModelCreatingNotCompatibleWithEffort(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        
        protected virtual void OnModelCreatingNotCompatibleWithEffort(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnType("varchar");
            modelBuilder.Entity<Stock>().Property(x => x.Name).HasColumnType("varchar");
        }
    }
}
