using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace projectManagementTool.DAO.AdoNet
{
    public class DefaultConnectionFactory : IConnectionFactory
    {
        private DbProviderFactory dbProviderFactory;

        public DefaultConnectionFactory(DbProviderFactory providerFactory, string providerName, string connectionString)
        {
            DbProviderFactories.RegisterFactory(providerName, providerFactory);

            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
            this.dbProviderFactory = providerFactory;
        }

        public string ConnectionString { get; }

        public string ProviderName { get; }

        public DbConnection CreateConnection()
        {
            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = this.ConnectionString;
            connection.Open();
            return connection;
        }
    }
}
