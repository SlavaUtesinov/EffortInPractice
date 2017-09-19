using Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class EffortDataContextFactory : IDataContextFactory
    {
        private readonly DbConnection Connection;
        public EffortDataContextFactory(DbConnection connection)
        {
            Connection = connection;
        }

        public DataContext CreateContext()
        {
            return new EffortContext(Connection);
        }
    }
}
