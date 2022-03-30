﻿using RMDataManager.Library.DataAccess;
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
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        [HttpGet]
        public List<ProductModel> GetAll()
        {
            ProductData data = new ProductData();

            return data.GetProducts();
        }
    }
}