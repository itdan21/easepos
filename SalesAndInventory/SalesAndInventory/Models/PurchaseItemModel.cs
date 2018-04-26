using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInventory.Models
{
    public class PurchaseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int QtyByPcks { get; set; }
        public int QtyByPcs { get; set; }
        
        public double UnitPrice { get; set; }
        public double Srp { get; set; }
        public int Vat { get; set; }

        public bool IsCarton { get; set; }
        public string CartonBarcode { get; set; }

        public int PurchaseId { get; set; }
        public int ProductId { get; set; }

        public static int Add(PurchaseItem model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.PurchaseItems.Add(model);
            db.SaveChanges();
            return model.Id;
        }
    }
}