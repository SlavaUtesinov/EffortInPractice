using Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public class Service
    {
        private IDataContextFactory Factory { get; set; }
        public Service(IDataContextFactory factory)
        {
            Factory = factory;
        }

        public int AddProduct(string name, decimal price, string category, int stockId)
        {
            using (var context = Factory.CreateContext())
            {
                var product = context.Products.Add(new Product
                { Name = name, Category = category, Price = price, StockId = stockId });
                context.SaveChanges();
                return product.Id;
            }
        }

        public void DeleteProduct(int id)
        {
            using (var context = Factory.CreateContext())
            {
                var product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
