using Context.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Migrations
{
    public class Seeder
    {
        public static void Seed(DataContext context)
        {            
            context.Stocks.AddOrUpdate(x => x.Name, new Stock { Name = "First" });
            context.Stocks.AddOrUpdate(x => x.Name, new Stock { Name = "Second" });
            context.SaveChanges();            
        }
    }
}
