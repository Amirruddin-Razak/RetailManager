﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RMCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SaleController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData(_config);
            data.SaveSale(sale, User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
        public List<SaleReportDBModel> GetSalesReport()
        {
            SaleData data = new SaleData(_config);
            return data.GetSaleReport();
        }
    }
}