using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInventory.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string PurchaseNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime DateAndTime { get; set; }
        public string ReceivedBy { get; set; }
        
        public decimal Expenses { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public int SupplierId { get; set; }

        public static int Add(Purchase model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Purchases.Add(model);
            db.SaveChanges();
            return model.Id;
        }
    }
}