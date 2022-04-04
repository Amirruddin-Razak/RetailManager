using RMDataManager.Library.Models;
using System.Collections.Generic;

namespace RMDataManager.Library.DataAccess
{
    public interface IProductData
    {
        ProductDBModel GetProductById(int id);
        List<ProductDBModel> GetProducts();
    }
}