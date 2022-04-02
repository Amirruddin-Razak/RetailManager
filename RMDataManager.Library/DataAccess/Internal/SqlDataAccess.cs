using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess.Internal
{
    internal class SqlDataAccess : IDisposable
    {
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnString(string name)
        {
            return _config.GetConnectionString(name);
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameter, string connectionStringName) 
        {
            List<T> rows;
            string connString = GetConnString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connString))
            {
                rows = connection.Query<T>(storedProcedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return rows;
        }

        public void SaveData<T>(string storedProcedure, T parameter, string connectionStringName)
        {
            string connString = GetConnString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connString))
            {
                connection.Execute(storedProcedure, parameter, commandType: CommandType.StoredProcedure);
            }
        }

        public void SaveData<T>(string storedProcedure, T parameter, string connectionStringName, out int id)
        {
            id = 0;
            string connString = GetConnString(connectionStringName);

            var p = new DynamicParameters(parameter);
            p.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (IDbConnection connection = new SqlConnection(connString))
            {
                connection.Execute(storedProcedure, p, commandType: CommandType.StoredProcedure);
                id = p.Get<int>("@Id");
            }
        }



        IDbConnection _connection;
        IDbTransaction _transaction;
        private readonly IConfiguration _config;

        public void StartTransaction(string connectionStringName)
        {
            string cnn = GetConnString(connectionStringName);
            _connection = new SqlConnection(cnn);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void RollBackTransaction()
        {
            _transaction.Rollback();
        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameter)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameter, commandType: CommandType.StoredProcedure,
                transaction: _transaction).ToList();

            return rows;
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameter)
        {
            _connection.Execute(storedProcedure, parameter, commandType: CommandType.StoredProcedure,
                transaction: _transaction);
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameter, out int id)
        {
            id = 0;

            var p = new DynamicParameters(parameter);
            p.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Output);

            _connection.Execute(storedProcedure, p, commandType: CommandType.StoredProcedure, transaction: _transaction);
            id = p.Get<int>("@Id");
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
