using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Context;
using Context.Migrations;
using Context.Models;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class UnitTests
    {
        private Service Service { get; set; }
        private DataContext Context { get; set; }
        private Stock Stock1 { get; set; }
        private Stock Stock2 { get; set; }       

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = new EffortDataContextFactory(Effort.DbConnectionFactory.CreateTransient());
            Context = factory.CreateContext();
            Seeder.Seed(Context);
            Stock1 = Context.Stocks.Where(x => x.Name == "First").Single();
            Stock2 = Context.Stocks.Where(x => x.Name == "Second").Single();
            Service = new Service(factory);
            TestSeed();
        }

        private void TestSeed()
        {
            Service.AddProduct("product1", 10, "category1", Stock1.Id);
            Service.AddProduct("product2", 20, "category1", Stock1.Id);
            Service.AddProduct("product3", 30, "category2", Stock1.Id);
            Service.AddProduct("product4", 40, "category2", Stock2.Id);
            Service.AddProduct("product5", 50, "category2", Stock2.Id);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Context.Dispose();
        }

        private void ReloadStocks()
        {
            Context.Entry(Stock1).Reload();
            Context.Entry(Stock2).Reload();
        }

        private void Asserts(params decimal[] asserts)
        {
            Assert.AreEqual(asserts[0], Context.Products.Sum(x => x.Price));
            Assert.AreEqual(asserts[1], Context.Products.Count());

            ReloadStocks();
            Assert.AreEqual(asserts[2], Stock1.TotalAmount);
            Assert.AreEqual(asserts[3], Stock2.TotalAmount);

            for(var i = 0; i < 3; i++)
            {
                var category = Context.CategoryBalances.FirstOrDefault(x => x.Category == "category" + (i + 1));
                Assert.AreEqual(asserts[4 + i * 2], category?.Amount ?? 0);
                Assert.AreEqual(asserts[5 + i * 2], category?.Quantity ?? 0);
            }            
        }

        [TestMethod]
        public void AddProducts()
        {
            Asserts(150, 5, 60, 90, 30, 2, 120, 3, 0, 0);
        }

        [TestMethod]
        public void DeleteAndUpdateProducts()
        {
            var productDelete = Context.Products.Where(x => x.Name == "product1").Single();
            Service.DeleteProduct(productDelete.Id);

            var productUpdate = Context.Products.Where(x => x.Name == "product2").Single();
            productUpdate.Price += 100;
            productUpdate.Category = "category3";

            ReloadStocks();
            Context.SaveChanges();

            Asserts(240, 4, 150, 90, 0, 0, 120, 3, 120, 1);
        }        
    }
}
