using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInventory.Models
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int SaleId { get; set; }

        public int? StockId { get; set; }
        public int? PackageId { get; set; }

        public double UnitPrice { get; set; }
        public int Discount { get; set; }
        public int Qty { get; set; }

    }
}