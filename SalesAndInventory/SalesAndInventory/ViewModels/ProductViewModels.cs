using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInventory.ViewModels
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public double UnitPrice { get; set; }
        public double Srp { get; set; }
        public double SellingPrice { get; set; }
        public int Vat { get; set; }
    }

    public class ProductListViewModel
    {
        public List<ProductInfo> Stocks { get; set; }
        public List<ProductInfo> UnassignedStoks { get; set; }
        public List<ProductInfo> OutOfStocks { get; set; }

        public int TotalQtyPc { get; set; }
        public int TotalQtyPck { get; set; }
        public int TotalQtyStock { get; set; }
        public double TotalSellingPrice { get; set; }

        public class ProductInfo : ProductCreateViewModel
        {
            public string ItemBarcode { get; set; }
            public string CartonBarcode { get; set; }
            public int QtyByPc { get; set; }
            public int QtyByPck { get; set; }
            public int TotalItemStocks { get; set; }
        }
    }
}