using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInventory.ViewModels
{
    public class PurchaseCreateViewModel
    {
        [Required]
        [Display(Name = "Purchase #")]
        public string PurchaseNumber { get; set; }
        [Display(Name = "Reference #")]
        public string ReferenceNumber { get; set; }
        [Display(Name = "Received By")]
        public string ReceivedBy { get; set; }

        [Required]
        public decimal Expenses { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public decimal PurchaseAmount { get; set; }
        [Required]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
    }

    public class PurchaseListViewModel
    {
        public List<PurchaseInfo> PurchaseList { get; set; }

        public class PurchaseInfo : PurchaseCreateViewModel
        {
            public int PurchaseId { get; set; }
            public DateTime DateAndTime { get; set; }
            public string SupplierName { get; set; }
        }
    }

    public class PurchaseDetailsViewModel : PurchaseCreateViewModel
    {
        public int PurchaseId { get; set; }
        public string SupplierName { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}