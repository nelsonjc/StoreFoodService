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


        public IEnumerable<T> ExecuteQuery<T>(string sql, object parameters = null)
        {
            using var connection = CreateConnection();
            connection.Open();
            return connection.Query<T>(sql, parameters);
        }

        public int ExecuteCommand(string sql, object parameters = null)
        {
            using var connection = CreateConnection();
            connection.Open();
            return connection.Execute(sql, parameters);
        }
    }
}
