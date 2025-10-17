using Invoice.Infrastructure.DbContext.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Polly;
using System.Data;

namespace Invoice.Infrastructure.DbContext
{
    public class AppDbContext : IAppDbContext
    {

        #region ATTRIBUTES
        private readonly IConfiguration _configuration;
        #endregion

        #region "CONSTRUCTOR"
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region "METODOS"
        public IDbConnection GetConnection()
        {
            Lazy<IDbConnection> lazyConnection = new(() =>
            {
                var connectionString = _configuration.GetConnectionString("SQLConnection");
                var connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            });

            var retryPolicy = Policy.Handle<SqlException>()
                                    .WaitAndRetry(7, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            try
            {
                return retryPolicy.Execute(() =>
                {
                    return lazyConnection.Value;
                });
            }
            catch (SqlException ex)
            {
                throw new Exception("Error connection DB.", ex);
            }
        }
        #endregion
    }
}
