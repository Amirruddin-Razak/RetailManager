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
    public class InventoryData
    {
        private readonly IConfiguration _config;

        public InventoryData(IConfiguration config)
        {
            _config = config;
        }

        public List<InventoryDBModel> GetInventories()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            List<InventoryDBModel> output = sql.LoadData<InventoryDBModel, dynamic>("dbo.spInventory_GetAll", new { }, "RMData");

            return output;
        }

        public void SaveInventory(InventoryDBModel inventory)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            sql.SaveData("dbo.spInventory_Insert", inventory, "RMData");
        }
    }
}
