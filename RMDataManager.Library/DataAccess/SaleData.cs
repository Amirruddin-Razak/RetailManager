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
    public class SaleData
    {
        private readonly IConfiguration _config;

        public SaleData(IConfiguration config)
        {
            _config = config;
        }

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleItemDBModel> saleItems = new List<SaleItemDBModel>();
            List<ProductDBModel> productToUpdate = new List<ProductDBModel>();
            ProductData data = new ProductData(_config);

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

            using (SqlDataAccess sql = new SqlDataAccess(_config))
            {
                try
                {
                    sql.StartTransaction("RMData");

                    sql.SaveDataInTransaction("dbo.spSale_Insert", sale, out int id);
                    sale.Id = id;

                    foreach (SaleItemDBModel item in saleItems)
                    {
                        item.SaleId = sale.Id;

                        sql.SaveDataInTransaction("dbo.spSaleItem_Insert", item);
                    }

                    foreach (ProductDBModel product in productToUpdate)
                    {
                        sql.SaveDataInTransaction("dbo.spProduct_Update",
                            new { product.Id, product.QuantityInStock, product.ReservedQuantity });
                    }

                    sql.CommitTransaction();
                }
                catch (Exception)
                {
                    sql.RollBackTransaction();
                    throw;
                }
            }
        }

        public List<SaleReportDBModel> GetSaleReport()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            List<SaleReportDBModel> output = sql.LoadData<SaleReportDBModel, dynamic>("dbo.spSale_SaleReport", new { }, "RMData");

            return output;
        }
    }
}
