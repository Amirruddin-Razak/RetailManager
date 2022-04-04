using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryData _data;

        public InventoryController(IInventoryData data)
        {
            _data = data;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post(InventoryDBModel inventory)
        {
            _data.SaveInventory(inventory);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public List<InventoryDBModel> Get()
        {
            return _data.GetInventories();
        }
    }
}
