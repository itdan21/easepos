using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesAndInventory.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }

        public static int Add(Supplier model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Suppliers.Add(model);
            db.SaveChanges();

            return model.Id;
        }
    }
}