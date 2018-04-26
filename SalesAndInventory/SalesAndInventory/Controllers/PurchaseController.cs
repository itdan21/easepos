using SalesAndInventory.Models;
using SalesAndInventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInventory.Controllers
{
    public class PurchaseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = new PurchaseListViewModel();

            model.PurchaseList = db.Purchases.Select(p => new PurchaseListViewModel.PurchaseInfo
            {
                PurchaseId = p.Id,
                PurchaseNumber = p.PurchaseNumber,
                DateAndTime = p.DateAndTime,
                PurchaseAmount = p.PurchaseAmount,
                TotalAmount = p.TotalAmount,
                Expenses = p.Expenses,
                ReceivedBy = p.ReceivedBy,
                ReferenceNumber = p.ReferenceNumber,
                SupplierName = db.Suppliers.Where(i => i.Id == p.SupplierId).Select(i => i.Name).FirstOrDefault()
            }).ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.SupplierList = db.Suppliers.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(PurchaseCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var purchase = new Purchase();

                if (db.Purchases.Where(p => p.PurchaseNumber == model.PurchaseNumber).Any())
                {
                    ViewBag.Message = "Purchase # already exist in the database.";
                }
                else
                {
                    purchase.ReferenceNumber = model.ReferenceNumber;
                    purchase.PurchaseNumber = model.PurchaseNumber;
                    purchase.DateAndTime = DateTime.Now;
                    purchase.ReceivedBy = model.ReceivedBy;
                    purchase.TotalAmount = model.TotalAmount;
                    purchase.Expenses = model.Expenses;
                    purchase.PurchaseAmount = model.PurchaseAmount;
                    purchase.SupplierId = model.SupplierId;
                    int purchaseId = Purchase.Add(purchase);

                    ViewBag.PurchaseNumber = model.PurchaseNumber;

                    return RedirectToAction("Details", new { id = purchaseId });
                }
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            var model = new PurchaseDetailsViewModel();

            model = db.Purchases.Where(p => p.Id == id)
                                .GroupJoin(db.Suppliers, pr => pr.SupplierId, s => s.Id, (pr, s) => new PurchaseDetailsViewModel
                                {
                                    PurchaseId = pr.Id,
                                    ReferenceNumber = pr.ReferenceNumber,
                                    PurchaseNumber = pr.PurchaseNumber,
                                    ReceivedBy = pr.ReceivedBy,
                                    Expenses = pr.Expenses,
                                    PurchaseAmount = pr.PurchaseAmount,
                                    TotalAmount = pr.TotalAmount,
                                    DateAndTime = pr.DateAndTime,
                                    SupplierName = s.Where(a => a.Id == pr.SupplierId).Select(a => a.Name).FirstOrDefault()
                                }).FirstOrDefault();

            return View(model);
        }



        public JsonResult AddPurchaseItem(string Name, string Brand, string Model, int QtyByPc, int QtyByPck, bool IsCrtn, string BarcodeCrtn, string BarcodePck, string BarcodePc, int PurchaseId, double UnitPrice)
        {
            if (Name == "" || Brand == "")
            {
                return Json("Please enter Item Name and Item Brand", JsonRequestBehavior.AllowGet);
            }

            // Adding to Product Table
            var product = new Product();
            product.Name = Name;
            product.Brand = Brand;
            product.Model = Model;
            int productId = Product.Add(product);

            // Adding to Purchase Item Table
            var purchaseItem = new PurchaseItem();
            purchaseItem.PurchaseId = PurchaseId;
            purchaseItem.Name = Name;
            purchaseItem.Brand = Brand;
            purchaseItem.Model = Model;
            purchaseItem.QtyByPcs = QtyByPc;
            purchaseItem.QtyByPcks = QtyByPck;
            purchaseItem.UnitPrice = UnitPrice;
            purchaseItem.ProductId = productId;
            purchaseItem.IsCarton = IsCrtn;
            purchaseItem.CartonBarcode = BarcodeCrtn;
            int purchaseItemId = PurchaseItem.Add(purchaseItem);

            int packId = 0;
            if (QtyByPck >= 1)
            {
                var pack = new StockByPack();
                pack.Barcode = BarcodePck;
                pack.QtyByPcs = QtyByPc;
                pack.PurchaseItemId = purchaseItemId;
                packId = StockByPack.Add(pack);
            }

            int counter = QtyByPc * QtyByPck;
            for(int i=0; i < counter; i++)
            {
                var item = new StockByItem();
                item.BarcodeNumber = BarcodePc;
                item.IsSold = false;
                item.ProductId = productId;
                item.SellingPrice = 0;
                item.StockByPackId = packId;
                item.PurchaseItemId = purchaseItemId;
                StockByItem.Add(item);
            }
            
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurchaseItemList(int purchaseId)
        {
            var model = db.PurchaseItems.Where(i => i.PurchaseId == purchaseId).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItemDetails(string itemBarcode)
        {
            if (db.StockByItems.Where(i => i.BarcodeNumber == itemBarcode).Any())
            {
                var model = db.StockByItems.Where(i => i.BarcodeNumber == itemBarcode)
                    .Join(db.Products, s => s.ProductId, p => p.Id, (s, p) => new
                    {
                        p.Name,
                        p.Model,
                        p.Brand,
                        s.PurchaseItemId
                    })
                    .Join(db.PurchaseItems, a => a.PurchaseItemId, b => b.Id, (a,b) => new
                    {
                        a.Name,
                        a.Model,
                        a.Brand,
                        b.UnitPrice,
                        b.Srp
                    })
                    .FirstOrDefault();

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}