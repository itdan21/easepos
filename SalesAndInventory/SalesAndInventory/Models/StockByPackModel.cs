using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInventory.Models
{
    public class StockByPack
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public int QtyByPcs { get; set; }

        public int PurchaseItemId { get; set; }

        public static int Add(StockByPack model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.StockByPacks.Add(model);
            db.SaveChanges();
            return model.Id;
        }
    }
}