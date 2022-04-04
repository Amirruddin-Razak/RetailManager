using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RMDataManager.Library.DataAccess.Internal;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData : ISaleData
    {
        private readonly IProductData _data;
        private readonly ISqlDataAccess _sql;
        private readonly ILogger _logger;

        public SaleData(IProductData data, ISqlDataAccess sql, ILogger<SaleData> logger)
        {
            _data = data;
            _sql = sql;
            _logger = logger;
        }

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleItemDBModel> saleItems = new List<SaleItemDBModel>();
            List<ProductDBModel> productToUpdate = new List<ProductDBModel>();

            foreach (SaleItemModel si in saleInfo.SaleItems)
            {
                SaleItemDBModel item = new SaleItemDBModel
                {
                    ProductId = si.ProductId,
                    Quantity = si.Quantity
                };

                ProductDBModel productInfo = _data.GetProductById(item.ProductId);
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

            try
            {
                _sql.StartTransaction("RMData");

                _sql.SaveDataInTransaction("dbo.spSale_Insert", sale, out int id);
                sale.Id = id;

                foreach (SaleItemDBModel item in saleItems)
                {
                    item.SaleId = sale.Id;

                    _sql.SaveDataInTransaction("dbo.spSaleItem_Insert", item);
                }

                foreach (ProductDBModel product in productToUpdate)
                {
                    _sql.SaveDataInTransaction("dbo.spProduct_Update",
                        new { product.Id, product.QuantityInStock, product.ReservedQuantity });
                }

                _sql.CommitTransaction();
            }
            catch (Exception ex)
            {
                _sql.RollBackTransaction();
                _logger.LogError(ex, "Check out transaction fail when accessing SQL. Changes are rolled back");
                throw;
            }
        }

        public List<SaleReportDBModel> GetSaleReport()
        {
            List<SaleReportDBModel> output = _sql.LoadData<SaleReportDBModel, dynamic>("dbo.spSale_SaleReport", new { }, "RMData");

            return output;
        }
    }
}
