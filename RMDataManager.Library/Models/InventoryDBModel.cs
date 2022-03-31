using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.Models
{
    public class InventoryDBModel
    {
        public int ProductId { get; set; }
        public decimal PurchasePrice { get; set; }
        public int PurchaseQuantity { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
