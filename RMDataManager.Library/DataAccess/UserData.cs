using Microsoft.Extensions.Configuration;
using RMDataManager.Library.DataAccess.Internal;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public UserDBModel GetUserById(string Id)
        {
            List<UserDBModel> output = _sql.LoadData<UserDBModel, dynamic>("dbo.spUserLookup", new { Id }, "RMData");

            return output.First();
        }
    }
}
