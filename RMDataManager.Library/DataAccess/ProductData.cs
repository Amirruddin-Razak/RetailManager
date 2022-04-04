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
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _sql;

        public ProductData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<ProductDBModel> GetProducts()
        {
            List<ProductDBModel> result = _sql.LoadData<ProductDBModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");

            return result;
        }

        public ProductDBModel GetProductById(int Id)
        {
            ProductDBModel result = _sql.LoadData<ProductDBModel, dynamic>("dbo.spProduct_GetById", new { Id }, "RMData").First();

            return result;
        }
    }
}
