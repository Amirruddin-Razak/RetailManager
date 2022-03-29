using Dapper;
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
    internal class SqlDataAccess
    {
        private string GetConnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
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
    }
}
