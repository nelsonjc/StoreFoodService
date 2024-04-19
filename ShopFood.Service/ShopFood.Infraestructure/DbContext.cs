using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ShopFood.Infraestructure
{
    public abstract class DbContext
    {
        protected string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DbConnectionStrings"];
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        protected async Task<T> GetAsyncFirst<T>(string query) where T : new()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<T>(query).FirstOrDefault();
        }

        protected async Task<IEnumerable<T>> ExecuteQuery<T>(string sql, object parameters = null)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();
            var result = await connection.QueryAsync<T>(sql, parameters);
            await connection.CloseAsync();
            return result;
        }

        protected async Task ExecuteCommand(string sql, object parameters = null)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, parameters);
            await connection.CloseAsync();
        }
    }
}
