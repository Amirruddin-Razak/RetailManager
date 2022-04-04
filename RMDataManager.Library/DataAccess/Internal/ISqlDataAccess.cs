using System.Collections.Generic;

namespace RMDataManager.Library.DataAccess.Internal
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        void Dispose();
        List<T> LoadData<T, U>(string storedProcedure, U parameter, string connectionStringName);
        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameter);
        void RollBackTransaction();
        void SaveData<T>(string storedProcedure, T parameter, string connectionStringName);
        void SaveData<T>(string storedProcedure, T parameter, string connectionStringName, out int id);
        void SaveDataInTransaction<T>(string storedProcedure, T parameter);
        void SaveDataInTransaction<T>(string storedProcedure, T parameter, out int id);
        void StartTransaction(string connectionStringName);
    }
}