using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using AC = ShopFood.Domain.Variables.AppConfig;

namespace ShopFood.Infraestructure
{
    /// <summary>
    /// Class to data access generic
    /// </summary>
    /// <remarks>
    /// Ctor to DbContext
    /// </remarks>
    /// <param name="configuration">Parameter to get information from app settings type of Microsoft.Extensions.Configuration </param>
    public abstract class DbContext(IConfiguration configuration)
    {
        #region Variables
        /// <summary>
        /// Variable with connection string value
        /// </summary>
        protected string? _connectionString = configuration[AC.AppSeeting_ConnectionString];
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to get a generic object from data base
        /// </summary>
        /// <typeparam name="T">Generic parameter</typeparam>
        /// <param name="query">Parameter with script to execute</param>
        /// <returns>Result with database information</returns>
        protected async Task<T?> GetAsyncFirst<T>(string query) where T : new()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var result = db.Query<T?>(query).FirstOrDefault();
            await Task.CompletedTask;
            return result;
        }

        /// <summary>
        /// Method to Execute a Query in data base
        /// </summary>
        /// <typeparam name="T">Generic parameter</typeparam>
        /// <param name="sql">Parameter with script to execute</param>
        /// <param name="parameters">Parameter with params in sql</param>
        /// <returns>Result list with database information</returns>
        protected async Task<IEnumerable<T>> ExecuteQuery<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();
            var result = await connection.QueryAsync<T>(sql, parameters);
            await connection.CloseAsync();
            return result;
        }

        /// <summary>
        /// Method to Execute Command
        /// </summary>
        /// <param name="sql">Parameter with script to execute</param>
        /// <param name="parameters">Parameter with params in sql</param>
        protected async Task ExecuteCommand(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, parameters);
            await connection.CloseAsync();
        } 
        #endregion

        #region Private Methods
        /// <summary>
        /// Method to get instance SqlConnection
        /// </summary>
        /// <returns>SqlConnection data</returns>
        private SqlConnection CreateConnection() => new(_connectionString); 
        #endregion
    }
}
