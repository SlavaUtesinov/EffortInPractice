using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public interface IDataContextFactory
    {
        DataContext CreateContext();
    }

    public class DataContextFactory : IDataContextFactory
    {
        public DataContext CreateContext()
        {
            return new DataContext();
        }
    }
}
