using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Encord.Common.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Encord.Common
{
    public class SqlDbContext
    {
        #region Fields
        private readonly SqlConnection _conn;
        private readonly ILogger<SqlDbContext> _logger;
        #endregion

        #region Setup
        public SqlDbContext(IOptions<SQLSettings> _SqlSettings, ILogger<SqlDbContext> logger)
        {
            _logger = logger;
            _conn = new SqlConnection(_SqlSettings.Value.ConnectionString);
        }
        #endregion

        #region Methods
        public ConnectionState GetConnectionState()
        {
            return _conn.State;
        }

        public SqlConnection OpenConnection()
        {
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                    _logger.LogInformation("Opened connection to database");
                }
                else
                {
                    _logger.LogInformation("Already opened connection to database");
                }
            }
            catch (Exception exp)
            {
                _logger.LogInformation("Couldn't connect to database: " + exp);
                throw;
            }
            return _conn;
        }

        public bool CloseConnection()
        {
            bool result = false;
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
                result = true;
            }
            _logger.LogInformation("Closed connection to database");
            return result;
        }


        public async Task<DataSet> GetDataAsync(string procedure, SqlCommand cmd)
        {
            try
            {
                cmd.Connection = OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedure;

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                CloseConnection();
                return ds;
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failed GetDataAsync for {cmdname}", procedure);
                throw;
            }
        }

        public int ExecuteNonQuery(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failed ExecuteNonQuery for {cmdname}", cmd.CommandText);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<SqlDataReader> ExecuteReaderAsync(SqlCommand cmd)
        {
            try
            {
                return await cmd.ExecuteReaderAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failed ExecuteReaderAsync for {cmdname}", cmd.CommandText);
                CloseConnection();
                throw;
            }

        }
        #endregion
    }
}
