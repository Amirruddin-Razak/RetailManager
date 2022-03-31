using RMDataManager.Library.DataAccess.Internal;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class UserData
    {
        public UserDBModel GetUserById(string id)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var p = new { Id = id };

            List<UserDBModel> output = sql.LoadData<UserDBModel, dynamic>("dbo.spUserLookup", p, "RMData");

            return output.First();
        }
    }
}
