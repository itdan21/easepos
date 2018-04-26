using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInventory.ViewModels
{
    public class SaleCreateViewModel
    {
        public int CustomerCode { get; set; }
        public string CustomerFN { get; set; }

        public DateTime DateTime { get; set; }
        public double TotalAmount { get; set; }

        public List<ItemDetail> ItemList { get; set; }
        public class ItemDetail
        {
            public int PackageId { get; set; }
            public int StockId { get; set; }
            public double UnitPrice { get; set; }
            public int Qty { get; set; }
            public int Discount { get; set; }
        }
    }
}