using SalesAndInventory.Models;
using SalesAndInventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInventory.Controllers
{
    public class SupplierController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = new SupplierListViewModel();

            model.SupplierList = db.Suppliers.Select(s => new SupplierListViewModel.SupplierInfo
                                                    {
                                                        Id = s.Id,
                                                        Name = s.Name,
                                                        Address = s.Address,
                                                        ContactEmail = s.ContactEmail,
                                                        ContactName = s.ContactName,
                                                        ContactNumber = s.ContactNumber
                                                    }).ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SupplierAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier();

                if (db.Suppliers.Where(s => s.Name == model.Name).Any())
                {
                    ViewBag.Message = "Supplier already exist from the database.";
                }
                else
                {
                    supplier.Name = model.Name;
                    supplier.Address = model.Address;
                    supplier.ContactName = model.ContactName;
                    supplier.ContactEmail = model.ContactEmail;
                    supplier.ContactNumber = model.ContactNumber;
                    Supplier.Add(supplier);

                    ViewBag.Supplier = model.Name;
                }
            }

            return View();
        }
    }
}