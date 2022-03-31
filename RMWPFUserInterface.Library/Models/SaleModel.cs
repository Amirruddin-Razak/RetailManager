using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWPFUserInterface.Library.Models
{
    public class SaleModel
    {
        public List<SaleItemModel> SaleItems { get; set; } = new List<SaleItemModel>();
    }
}
