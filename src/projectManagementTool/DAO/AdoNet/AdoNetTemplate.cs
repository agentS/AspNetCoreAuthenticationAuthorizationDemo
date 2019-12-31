using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace projectManagementTool.DAO.AdoNet
{
    public delegate T RowMapper<T>(IDataRecord row);

    public sealed class AdoNetTemplate
    {
        private readonly IConnectionFactory connectionFactory;

        public AdoNetTemplate(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<int> ExecuteStatementAsync(string sql, params Parameter[] parameters)
        {
            using (DbConnection connection = this.connectionFactory.CreateConnection())
            using (DbCommand statement = connection.CreateCommand())
            {
                statement.CommandText = sql;
                AddParameters(statement, parameters);

                return await statement.ExecuteNonQueryAsync();
            }
        }

        private static void AddParameters(DbCommand query, Parameter[] parameters)
        {
            foreach (Parameter processedParameter in parameters)
            {
                DbParameter dbParameter = query.CreateParameter();
                dbParameter.ParameterName = processedParameter.Name;
                dbParameter.Value = processedParameter.Value;

                query.Parameters.Add(dbParameter);
            }
        }

        const string QUERY_LATEST_ID = "SELECT last_insert_rowid()";
        public async Task<long> QueryLatestIdAsync()
        {
            return ((long)(await this.ExecuteScalarQueryAsync(QUERY_LATEST_ID)));
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, RowMapper<T> rowMapper, params Parameter[] parameters)
        {
            using (DbConnection connection = this.connectionFactory.CreateConnection())
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                AddParameters(command, parameters);

                List<T> items = new List<T>();
                using (DbDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(rowMapper(reader));
                    }

                    return items;
                }
            }
        }

        public async Task<long> ExecuteScalarQueryAsync(string sql, params Parameter[] parameters)
        {
            using (DbConnection connection = this.connectionFactory.CreateConnection())
            using (DbCommand query = connection.CreateCommand())
            {
                query.CommandText = sql;
                AddParameters(query, parameters);

                try
                {
                    return ((long)await query.ExecuteScalarAsync());
                }
                catch (InvalidCastException)
                {
                    return ((int)await query.ExecuteScalarAsync());
                }
            }
        }
    }
}
