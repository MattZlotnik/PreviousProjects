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

    public enum ChosenReport { SeatsSold, Revenue, Both }

    public enum TimeOfDay { AllDay, Morning, Afternoon, Evening, Night }

    public class TicketsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Tickets
        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketID,Seat,Taken,PriceAtPayment")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketID,Seat,Taken,PriceAtPayment")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // TODO: make button for this somewhere
        public ActionResult TicketDetailedSearch()
        {
            //ViewBag.AllMPPAARatings = GetAllMPAARatings();
            return View();
        }

        public ActionResult DisplayTicketSearchResults(ChosenReport RevenueOrSeats, DateTime? datBeginningDate, DateTime? datEndingDate, String MovieSearchString, MPAARating SelectedMPAARating, TimeOfDay Daytime)
        {
            var query = from t in db.Tickets
                        select t;

            query = query.Where(t => t.Order.Gift == false);
            query = query.Where(t => t.Order.CompletedOrder == true);
            query = query.Where(t => t.Order.CancelledOrder == false);
            query = query.Where(t => t.Order.PaymentMethod == PaymentMethod.Card);
            query = query.Where(t => t.Taken == true);

            if (datBeginningDate != null)
            {
                DateTime datBeginning = datBeginningDate ?? new DateTime(1900, 1, 1);
                query = query.Where(t => t.Showtime.Schedule.ScheduleDate >= datBeginningDate);
            }
            if (datEndingDate != null)
            {
                DateTime datEnding = datEndingDate ?? DateTime.Today;
                query = query.Where(t => t.Showtime.Schedule.ScheduleDate <= datEndingDate);
            }

            if (MovieSearchString != null && MovieSearchString != "")
            {
                query = query.Where(t => t.Showtime.Movie.Title.Contains(MovieSearchString));
            }

            if (SelectedMPAARating != MPAARating.All)
            {
                query = query.Where(t => t.Showtime.Movie.MPAARating == SelectedMPAARating);
            }

            if (Daytime != TimeOfDay.AllDay)
            {
                DateTime EndMorningTime = new DateTime(2000, 1, 1, 12, 0, 0);
                DateTime EndAfternoonTime = new DateTime(2000, 1, 1, 17, 0, 0);
                DateTime EndEveningTime = new DateTime(2000, 1, 1, 20, 0, 0);
                switch (Daytime)
                {
                    case TimeOfDay.Morning:
                        query = query.Where(t => t.Showtime.StartTime.Hour < EndMorningTime.Hour);
                        break;

                    case TimeOfDay.Afternoon:
                        query = query.Where(t => t.Showtime.StartTime.Hour >= EndMorningTime.Hour);
                        query = query.Where(t => t.Showtime.StartTime.Hour < EndAfternoonTime.Hour);
                        break;

                    case TimeOfDay.Evening:
                        query = query.Where(t => t.Showtime.StartTime.Hour >= EndAfternoonTime.Hour);
                        query = query.Where(t => t.Showtime.StartTime.Hour < EndEveningTime.Hour);
                        break;

                    case TimeOfDay.Night:
                        query = query.Where(t => t.Showtime.StartTime.Hour >= EndEveningTime.Hour);
                        break;
                }
            }

            List<Ticket> Selected = query.ToList();
            Decimal decTotalRevenue = (Selected.Sum(item => item.PriceAtPayment))*1.0825m;
            String strTotalRevenue = decTotalRevenue.ToString("C");
            Int32 intTotalSeats = Selected.Count();

            switch (RevenueOrSeats)
            {
                case ChosenReport.Both:
                    ViewBag.TotalRevenue = strTotalRevenue;
                    ViewBag.TotalSeats = intTotalSeats;
                    break;

                case ChosenReport.Revenue:
                    ViewBag.TotalRevenue = strTotalRevenue;
                    break;

                case ChosenReport.SeatsSold:
                    ViewBag.TotalSeats = intTotalSeats;
                    break;
                //TODO: Delete this comment
            }

            return View("SummaryReport");
            
        }


        //TODO: i don´'t think we need this
        public SelectList GetAllMPAARatings()
        {
            List<MPAARating> MPAARatingsList = new List<MPAARating>();
            MPAARatingsList.Add(MPAARating.All);
            MPAARatingsList.Add(MPAARating.G);
            MPAARatingsList.Add(MPAARating.PG);
            MPAARatingsList.Add(MPAARating.PG13);
            MPAARatingsList.Add(MPAARating.R);
            MPAARatingsList.Add(MPAARating.NC17);
            MPAARatingsList.Add(MPAARating.Unrated);

            SelectList AllMPAARatings = new SelectList(MPAARatingsList);
            return AllMPAARatings;
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult PopcornPointsReport()
        {
            var query = from t in db.Tickets
                        select t;

            query = query.Where(t => t.Taken == true);
            query = query.Where(t => t.Order.CompletedOrder == true);
            query = query.Where(t => t.Order.CancelledOrder == false);
            query = query.Where(t => t.Order.PaymentMethod == PaymentMethod.PopcornPoints);

            List<Ticket> PopcornTickets = query.ToList();
            return View(PopcornTickets.OrderByDescending(t => t.Order.OrderDate));
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
