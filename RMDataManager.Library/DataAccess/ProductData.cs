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
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();
            
            List<ProductModel> result = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");

            return result;
        }
    }
}
