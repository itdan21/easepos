using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInventory.Models
{
    public class StockByItem
    {
        public int Id { get; set; }

        public string BarcodeNumber { get; set; }
        public bool IsSold { get; set; }
        public DateTime? DateAndTimeSold { get; set; }

        public double SellingPrice { get; set; }

        public int ProductId { get; set; }
        public int StockByPackId { get; set; }
        public int PurchaseItemId { get; set; }

        public static int Add(StockByItem model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.StockByItems.Add(model);
            db.SaveChanges();
            return model.Id;
        }
    }
}