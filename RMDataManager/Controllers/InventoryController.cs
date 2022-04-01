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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post(InventoryDBModel inventory)
        {
            InventoryData data = new InventoryData();
            data.SaveInventory(inventory);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public List<InventoryDBModel> Get()
        {
            InventoryData data = new InventoryData();
            return data.GetInventories();
        }
    }
}
