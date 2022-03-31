using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RMDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiController
    {
        [HttpPost]
        public void Post(InventoryDBModel inventory)
        {
            InventoryData data = new InventoryData();
            data.SaveInventory(inventory);
        }

        [HttpGet]
        public List<InventoryDBModel> Get()
        {
            InventoryData data = new InventoryData();
            return data.GetInventories();
        }
    }
}
