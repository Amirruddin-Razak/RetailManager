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
    public class ProductData
    {
        private readonly IConfiguration _config;

        public ProductData(IConfiguration config)
        {
            _config = config;
        }

        public List<ProductDBModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            
            List<ProductDBModel> result = sql.LoadData<ProductDBModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");

            return result;
        }

        public ProductDBModel GetProductById(int id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            ProductDBModel result = sql.LoadData<ProductDBModel, dynamic>("dbo.spProduct_GetById", new { Id = id }, "RMData").First();

            return result;
        }
    }
}
