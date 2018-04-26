
using SalesAndInventory.Models;
using SalesAndInventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInventory.Controllers
{
    public class SaleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }



        public JsonResult AddSaleItem(string barcode, int qty)
        {
            //var getStock = db.Stocks.Where(i => i.BarcodeNumber == barcode);

            //if(getStock.Count() >= qty)
            //{
            //    var itemModel = getStock.Join(db.Products, s => s.ProductId, p => p.Id, (s, p) => new
            //    {
            //        ProductName = p.Name,
            //        //SellingPrice = p.SellingPrice,
            //        Qty = qty
            //    }).FirstOrDefault();

            //    return Json(itemModel, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{

            //}

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}