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
    public class InventoryData : IInventoryData
    {
        private readonly ISqlDataAccess _sql;

        public InventoryData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<InventoryDBModel> GetInventories()
        {
            List<InventoryDBModel> output = _sql.LoadData<InventoryDBModel, dynamic>("dbo.spInventory_GetAll", new { }, "RMData");

            return output;
        }

        public void SaveInventory(InventoryDBModel inventory)
        {
            _sql.SaveData("dbo.spInventory_Insert", inventory, "RMData");
        }
    }
}
