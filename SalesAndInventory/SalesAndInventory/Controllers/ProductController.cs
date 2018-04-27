using SalesAndInventory.Models;
using SalesAndInventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInventory.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var stock = new ProductListViewModel();

            // Stocks
            stock.Stocks = db.PurchaseItems
                            .GroupJoin(db.StockByItems, pi => pi.Id, sp => sp.PurchaseItemId, (pi, sp) => new
                            {
                                Id = pi.Id,
                                Name = pi.Name,
                                Model = pi.Model,
                                Brand = pi.Brand,
                                CartonBarcode = pi.CartonBarcode,
                                QtyByPck = pi.QtyByPcks,
                                QtyByPc = pi.QtyByPcs,
                                SellingPrice = sp.Select(i => i.SellingPrice).FirstOrDefault(),
                                TotalItemStocks = sp
                            }).AsEnumerable()
                            .Select(i => new ProductListViewModel.ProductInfo
                            {
                                Id = i.Id,
                                Name = i.Name,
                                Model = i.Model,
                                Brand = i.Brand,
                                CartonBarcode = i.CartonBarcode,
                                QtyByPck = i.QtyByPck,
                                QtyByPc = i.QtyByPc,
                                TotalItemStocks = i.TotalItemStocks.Count(),
                                SellingPrice = i.SellingPrice
                            })
                            .ToList();

            stock.TotalSellingPrice = stock.Stocks.Select(i => i.SellingPrice).Sum();


            // Out of Stocks
            stock.OutOfStocks = db.Products
                                    .GroupJoin(db.StockByItems, p => p.Id, s => s.ProductId, (p, s) => new
                                    {
                                        p,
                                        s
                                    }).Where(i => i.s.Count() == 0).AsEnumerable()
                                    .Select(i => new ProductListViewModel.ProductInfo
                                    {
                                        Id = i.p.Id,
                                        Brand = i.p.Brand,
                                        Name = i.p.Name,
                                        Model = i.p.Model,
                                        ItemBarcode = i.s.Select(r => r.BarcodeNumber).FirstOrDefault(),
                                        QtyByPc = i.s.Count(),
                                        SellingPrice = i.s.Select(r => r.SellingPrice).FirstOrDefault()
                                    }).ToList();


            // Unassigned Stocks
            stock.UnassignedStoks = db.Products
                            .Join(db.StockByItems.Where(x => x.SellingPrice == 0), p => p.Id, s => s.ProductId, (p, s) => new
                            {
                                p.Id,
                                p.Name,
                                p.Brand,
                                p.Model,
                                s.SellingPrice,
                                s.ProductId,
                                s.PurchaseItemId,
                            }).AsEnumerable()
                            .Join(db.PurchaseItems, ps => ps.PurchaseItemId, pi => pi.Id, (ps,pi) => new
                            {
                                ps.Id,
                                ps.Name,
                                ps.Brand,
                                ps.SellingPrice,
                                ps.ProductId,
                                ps.PurchaseItemId,
                                pi.Srp,
                                pi.UnitPrice
                            })
                            .GroupBy(i => i.Id)
                            .Select(i => new ProductListViewModel.ProductInfo
                            {
                                Id = i.Key,
                                Name = i.Select(e => e.Name).FirstOrDefault(),
                                Brand = i.Select(e => e.Brand).FirstOrDefault(),
                                Srp = i.Select(e => e.Srp).FirstOrDefault(),
                                UnitPrice = i.Select(e => e.UnitPrice).FirstOrDefault(),
                                SellingPrice = i.Select(e => e.SellingPrice).FirstOrDefault(),
                                TotalItemStocks = i.Count()
                            })
                            .ToList();

            return View(stock);
        }




        public JsonResult UpdateProductValues(double sellingPrice, int productId)
        {

            using (var db = new ApplicationDbContext())
            {
                db.StockByItems
                    .Where(i => i.ProductId == productId)
                    .ToList()
                    .ForEach(a =>
                    {
                        a.SellingPrice = sellingPrice;
                    }
                            );
                db.SaveChanges();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetAvailableStocks()
        {
            var Stocks = db.PurchaseItems
                            .GroupJoin(db.StockByItems, pi => pi.Id, sp => sp.PurchaseItemId, (pi, sp) => new
                            {
                                Id = pi.Id,
                                ItemId = sp.Select(i => i.Id).FirstOrDefault(),
                                Name = pi.Name,
                                Model = pi.Model,
                                Brand = pi.Brand,
                                CartonBarcode = pi.CartonBarcode,
                                QtyByPck = pi.QtyByPcks,
                                QtyByPc = pi.QtyByPcs,
                                SellingPrice = sp.Select(i => i.SellingPrice).FirstOrDefault(),
                                UnitPrice = pi.UnitPrice,
                                SRP = pi.Srp,
                                TotalItemStocks = sp
                            }).AsEnumerable()
                            .Select(i => new
                            {
                                Id = i.Id,
                                ItemId = i.ItemId,
                                Name = i.Name,
                                Model = i.Model,
                                Brand = i.Brand,
                                CartonBarcode = i.CartonBarcode,
                                QtyByPck = i.QtyByPck,
                                QtyByPc = i.QtyByPc,
                                TotalItemStocks = i.TotalItemStocks.Count(),
                                SellingPrice = i.SellingPrice
                            })
                            .ToList();

            return Json(Stocks, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOutOfStocks()
        {
            var OutOfStocks = db.Products
                                    .GroupJoin(db.StockByItems, p => p.Id, s => s.ProductId, (p, s) => new
                                    {
                                        p,
                                        s
                                    }).Where(i => i.s.Count() == 0).AsEnumerable()
                                    .Select(i => new
                                    {
                                        Id = i.p.Id,
                                        Brand = i.p.Brand,
                                        Name = i.p.Name,
                                        Model = i.p.Model,
                                        ItemBarcode = i.s.Select(r => r.BarcodeNumber).FirstOrDefault(),
                                        QtyByPc = i.s.Count(),
                                        SellingPrice = i.s.Select(r => r.SellingPrice).FirstOrDefault()
                                    }).ToList();

            return Json(OutOfStocks, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnassignedStocks()
        {
            var unassignedStocks = db.Products
                            .Join(db.StockByItems.Where(x => x.SellingPrice == 0), p => p.Id, s => s.ProductId, (p, s) => new
                            {
                                p.Id,
                                p.Name,
                                p.Brand,
                                p.Model,
                                s.SellingPrice,
                                s.ProductId,
                                s.PurchaseItemId,
                            }).AsEnumerable()
                            .Join(db.PurchaseItems, ps => ps.PurchaseItemId, pi => pi.Id, (ps, pi) => new
                            {
                                ps.Id,
                                ps.Name,
                                ps.Brand,
                                ps.SellingPrice,
                                ps.ProductId,
                                ps.PurchaseItemId,
                                pi.Srp,
                                pi.UnitPrice
                            })
                            .GroupBy(i => i.Id)
                            .Select(i => new
                            {
                                Id = i.Key,
                                Name = i.Select(e => e.Name).FirstOrDefault(),
                                Brand = i.Select(e => e.Brand).FirstOrDefault(),
                                Srp = i.Select(e => e.Srp).FirstOrDefault(),
                                UnitPrice = i.Select(e => e.UnitPrice).FirstOrDefault(),
                                SellingPrice = i.Select(e => e.SellingPrice).FirstOrDefault(),
                                TotalItemStocks = i.Count()
                            })
                            .ToList();

            return Json(unassignedStocks, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult AddStock(int productId, int qty, string barcode)
        //{
        //    try
        //    {
        //        var stock = new Stock();
        //        var sellingPrice = db.Products.Where(i => i.Id == productId).Select(i => i.SellingPrice).FirstOrDefault();

        //        for (int i = 0; i < qty; i++)
        //        {
        //            stock.BarcodeNumber = barcode;
        //            stock.ProductId = productId;
        //            stock.IsSold = false;
        //            stock.SellingPrice = sellingPrice;
        //            Stock.Add(stock);
        //            db.SaveChangesAsync();
        //        }

        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult GetItemStocks(int purchaseId)
        {
            //var model = db.StockByItems.Where(i => i.PurchaseItemId == purchaseId)
            //    .Join(db.PurchaseItems, si => si.PurchaseItemId)
            //    .ToList();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}