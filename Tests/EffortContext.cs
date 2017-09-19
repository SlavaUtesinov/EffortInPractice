using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Common;
using Context.Models;
using Moq;

namespace Tests
{
    public class EffortContext : DataContext
    {
        protected override void OnModelCreatingNotCompatibleWithEffort(DbModelBuilder modelBuilder)
        {
        }

        public EffortContext(DbConnection connection) : base(connection)
        {
            MockCategoryBalance();
        }

        public override int SaveChanges()
        {
            MockProductTrigger();
            return base.SaveChanges();
        }        

        private void MockCategoryBalance()
        {
            var view = (from product in Products
                        group product by product.Category into sub
                        select new
                        {
                            sub.Key,
                            Amount = sub.Sum(x => x.Price),
                            Quantity = sub.Count()
                        }).AsEnumerable()
                        .Select(x => new CategoryBalance
                        {
                            Category = x.Key,
                            Amount = x.Amount,
                            Quantity = x.Quantity
                        }).AsQueryable();

            var mockSet = new Mock<DbSet<CategoryBalance>>();

            mockSet.As<IQueryable<CategoryBalance>>().Setup(m => m.Provider).Returns(view.Provider);
            mockSet.As<IQueryable<CategoryBalance>>().Setup(m => m.Expression).Returns(view.Expression);
            mockSet.As<IQueryable<CategoryBalance>>().Setup(m => m.ElementType).Returns(view.ElementType);
            mockSet.As<IQueryable<CategoryBalance>>().Setup(m => m.GetEnumerator()).Returns(() => view.GetEnumerator());
            mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(() => mockSet.Object);

            CategoryBalances = mockSet.Object;
        }

        private void MockProductTrigger()
        {            
            foreach (var item in ChangeTracker.Entries<Product>().Where(x => x.State != EntityState.Unchanged).ToList())
            {
                decimal delta = 0;
                var quantityProperty = item.Property(x => x.Price);
                switch (item.State)
                {
                    case EntityState.Deleted:
                        delta = -quantityProperty.CurrentValue;
                        break;
                    case EntityState.Added:
                        delta = quantityProperty.CurrentValue;
                        break;
                    default:
                        delta = quantityProperty.CurrentValue - quantityProperty.OriginalValue;
                        break;
                }
                var stock = Stocks.Find(item.Entity.StockId);                
                stock.TotalAmount += delta;
            }                            
        }        
    }
}
