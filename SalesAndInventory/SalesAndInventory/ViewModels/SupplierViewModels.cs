using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesAndInventory.ViewModels
{
    public class SupplierAddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string ContactName { get; set; }
        [Display(Name = "Contact No.")]
        [Required]
        public string ContactNumber { get; set; }
        [Display(Name = "Email")]
        [Required]
        public string ContactEmail { get; set; }
    }

    public class SupplierListViewModel
    {
        public List<SupplierInfo> SupplierList { get; set; }
        public class SupplierInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string ContactName { get; set; }
            public string ContactNumber { get; set; }
            public string ContactEmail { get; set; }
        }
    }
}