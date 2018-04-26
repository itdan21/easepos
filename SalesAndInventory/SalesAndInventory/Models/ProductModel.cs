using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInventory.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public static int Add(Product model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Products.Add(model);
            db.SaveChanges();
            return model.Id;
        }
    }
}