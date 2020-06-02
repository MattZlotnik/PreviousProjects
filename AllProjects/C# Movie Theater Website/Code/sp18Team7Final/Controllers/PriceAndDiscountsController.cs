using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sp18Team7Final.DAL;
using sp18Team7Final.Models;

namespace sp18Team7Final.Controllers
{
    [Authorize(Roles = "Manager")]
    public class PriceAndDiscountsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: PriceAndDiscounts
        public ActionResult Index()
        {
            return View(db.PriceAndDiscounts.ToList());
        }

        // GET: PriceAndDiscounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceAndDiscount priceAndDiscount = db.PriceAndDiscounts.Find(id);
            if (priceAndDiscount == null)
            {
                return HttpNotFound();
            }
            return View(priceAndDiscount);
        }

        // POST: PriceAndDiscounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriceAndDiscountID,Name,Amount")] PriceAndDiscount priceAndDiscount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(priceAndDiscount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(priceAndDiscount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
