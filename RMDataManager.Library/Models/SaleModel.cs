using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMDataManager.Library.Models
{
    public class SaleModel
    {
        public List<SaleItemModel> SaleItems { get; set; } = new List<SaleItemModel>();
    }
}