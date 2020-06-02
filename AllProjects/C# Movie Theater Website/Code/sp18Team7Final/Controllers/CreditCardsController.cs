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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace sp18Team7Final.Controllers
{
    public class CreditCardsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: CreditCards
        public ActionResult Index(String id)
        {
            List<CreditCard> AllCards = db.CreditCards.ToList();
            String currentuser = User.Identity.GetUserId();
            IEnumerable<CreditCard> UserCards = AllCards.Where(x => x.AppUser.Id == currentuser);
            return View(UserCards.ToList());
        }

        // GET: CreditCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // GET: CreditCards/Create
        public ActionResult Create()
        {
            String currentuserID = User.Identity.GetUserId();
            AppUser currentuser = db.Users.Find(currentuserID);
            var query = from card in currentuser.CreditCards
                        select card;
            List<CreditCard> selectedcards = query.ToList();
            int count = selectedcards.Count;
            if(count >= 2)
            {
                TempData["msg"] = "<script>alert('You have already added two credit cards. If you want to add another, please remove an existing card.');</script>";
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View();
        }

        // POST: CreditCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreditCardID,CardNumber,Expiration,CVV")] CreditCard creditCard, AppUser appuser)
        {
            
            if (ModelState.IsValid)
            {
                bool Validation = Utilities.ValidateCard.Validation(creditCard.CardNumber.ToString());
                if(Validation == false)
                {
                    TempData["msg"] = "<script>alert('This card number is not valid');</script>";
                    return Redirect(Request.UrlReferrer.ToString());
                }
                creditCard.CardType = Utilities.ValidateCard.GetCardType(creditCard.CardNumber.ToString());
                String currentuser = User.Identity.GetUserId();
                AppUser user = db.Users.First(x => x.Id == currentuser);
                creditCard.AppUser = user;
                creditCard.DisplayString = Utilities.ValidateCard.CreateHiddenNumberString(creditCard.CardNumber.ToString());
                db.CreditCards.Add(creditCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(creditCard);
        }

        // GET: CreditCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditCard creditCard = db.CreditCards.Find(id);
            db.CreditCards.Remove(creditCard);
            db.SaveChanges();
            return RedirectToAction("Index");
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
