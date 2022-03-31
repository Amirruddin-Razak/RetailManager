using RMDataManager.Library.DataAccess.Internal;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleItemDBModel> saleItems = new List<SaleItemDBModel>();
            List<ProductDBModel> productToUpdate = new List<ProductDBModel>();
            ProductData data = new ProductData();

            foreach (SaleItemModel si in saleInfo.SaleItems)
            {
                SaleItemDBModel item = new SaleItemDBModel
                {
                    ProductId = si.ProductId,
                    Quantity = si.Quantity
                };

                ProductDBModel productInfo = data.GetProductById(item.ProductId);
                productInfo.QuantityInStock -= item.Quantity;
                productToUpdate.Add(productInfo);

                if (productInfo == null)
                {
                    throw new Exception($"Item { item.ProductId } could not be found in the database");
                }

                item.SalePrice = item.Quantity * productInfo.RetailPrice;
                item.Tax = item.SalePrice * (productInfo.TaxPercentage / 100m);

                saleItems.Add(item);
            }

            SaleDBModel sale = new SaleDBModel
            {
                CashierId = cashierId,
                Subtotal = saleItems.Sum(x => x.SalePrice),
                Tax = saleItems.Sum(x => x.Tax)
            };

            sale.Total = sale.Subtotal + sale.Tax;

            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("spSale_Insert", sale, "RMData", out int id);
            sale.Id = id;

            foreach (SaleItemDBModel item in saleItems)
            {
                item.SaleId = sale.Id;

                sql.SaveData("spSaleItem_Insert", item, "RMData");
            }

            foreach (ProductDBModel product in productToUpdate)
            {
                sql.SaveData("spProduct_Update", new { product.Id, product.QuantityInStock, product.ReservedQuantity }, "RMData");
            }
        }
    }
}
