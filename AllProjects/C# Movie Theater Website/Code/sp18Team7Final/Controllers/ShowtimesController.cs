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

//TODO: Add link to this in Navbar
//TODO: Before submitting, create new database
//TODO: Create seed for PriceAndDiscounts
//TODO: Change to military time
namespace sp18Team7Final.Controllers
{
    public class ShowtimesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Showtimes
        public ActionResult Index()
        {
            var query = from st in db.Showtimes
                        select st;
            DateTime Today = DateTime.Now.Date;
            query = query.Where(st => st.Schedule.ScheduleDate == Today);
            List<Showtime> ShowtimesToDisplay = query.ToList();
            ShowtimesToDisplay.OrderBy(st => st.StartTime);
            ViewBag.DateMessage = Today.ToString("MM/dd/yyyy");
            if (ShowtimesToDisplay.Count == 0)
            {
                ViewBag.NoShowTime = "We're sorry, the schedule for this date has not been released";
            }
            return View(ShowtimesToDisplay);
        }

        // GET: Showtimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showtime showtime = db.Showtimes.Find(id);
            if (showtime == null)
            {
                return HttpNotFound();
            }
            return View(showtime);
        }

        // GET: Showtimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Showtimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShowtimeID,ShowtimeDate,StartTime,EndTime,Theater,Price,SpecialEvent,PriceOverride,CancelledShowtime")] Showtime showtime)
        {
            //TimeSpan matinee = new TimeSpan(12, 0, 0);
            //TimeSpan tuesDiscount = new TimeSpan(17, 0, 0);

            if (ModelState.IsValid)
            {
            //    if (showtime.StartTime.DayOfWeek == DayOfWeek.Monday || showtime.StartTime.DayOfWeek == DayOfWeek.Tuesday || showtime.StartTime.DayOfWeek == DayOfWeek.Wednesday || showtime.StartTime.DayOfWeek == DayOfWeek.Thursday || showtime.StartTime.DayOfWeek == DayOfWeek.Friday) 
            //    {
            //        if (showtime.StartTime.TimeOfDay < matinee)
            //        {
            //            showtime.Price = 5;
            //        }
            //    } 
            //    if (showtime.StartTime.DayOfWeek == DayOfWeek.Tuesday)
            //    {
            //        if (showtime.StartTime.TimeOfDay < tuesDiscount)
            //        {
            //            showtime.Price = 8;
            //        }
            //    }
            //    if (showtime.StartTime.DayOfWeek == DayOfWeek.Monday || showtime.StartTime.DayOfWeek == DayOfWeek.Tuesday || showtime.StartTime.DayOfWeek == DayOfWeek.Wednesday || showtime.StartTime.DayOfWeek == DayOfWeek.Thursday)
            //    {
            //        if (showtime.StartTime.TimeOfDay >= matinee)
            //        {
            //            showtime.Price = 10;
            //        }
            //    }
            //    if (showtime.StartTime.DayOfWeek == DayOfWeek.Friday)
            //    {
            //        if (showtime.StartTime.TimeOfDay >= matinee)
            //        {
            //            showtime.Price = 12;
            //        }
            //    }
            //    if (showtime.StartTime.DayOfWeek == DayOfWeek.Saturday || showtime.StartTime.DayOfWeek == DayOfWeek.Sunday)
            //    {
            //        if (showtime.StartTime.TimeOfDay >= matinee)
            //        {
            //            showtime.Price = 12;
            //        }
            //    }

                db.Showtimes.Add(showtime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(showtime);
        }

        // GET: Showtimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showtime showtime = db.Showtimes.Find(id);
            if (showtime == null)
            {
                return HttpNotFound();
            }
            return View(showtime);
        }

        // POST: Showtimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShowtimeID,ShowtimeDate,StartTime,EndTime,Theater,Price,SpecialEvent")] Showtime newshowtime, int SelectedTheater)
        {
            Showtime showtime = db.Showtimes.Find(newshowtime.ShowtimeID);
            Schedule oldschedule = db.Schedules.Find(showtime.Schedule.ScheduleID);

            // Time
            if (newshowtime.StartTime != null)
            {
                DateTime datNewDate = newshowtime.StartTime;

                int NewDayofYear = datNewDate.DayOfYear;

                var dayquery = from schedu in db.Schedules
                               select schedu;

                List<Schedule> SchedulesonDate = new List<Schedule>();

                foreach (Schedule sched in db.Schedules)
                {
                    if(sched.ScheduleDate.DayOfYear == NewDayofYear)
                    {
                        SchedulesonDate.Add(sched);
                    }
                }
                //dayquery = dayquery.Where(schedu => schedu.ScheduleDate.DayOfYear == NewDayofYear);

                if(SchedulesonDate.Count() == 0)
                {
                    ViewBag.TimeError = "There is no schedule on the selected date";
                    return View(showtime);
                }

                Schedule newSchedule = SchedulesonDate.First();

                if(datNewDate < DateTime.Now)
                {
                    ViewBag.TimeError = "You cannot reschedule a movie into the past";
                }

                int RelevantHours = newSchedule.ScheduleDate.Hour;
                int RelevantMinutes = newSchedule.ScheduleDate.Minute;
                TimeSpan Comparison = new TimeSpan(RelevantHours, RelevantMinutes, 0);
                DateTime ThisDate = newSchedule.ScheduleDate.Subtract(Comparison);

                //after 9am and end before midnight
                DateTime EarliestTime = ThisDate.AddHours(9);
                DateTime LatestTime = ThisDate.AddHours(24);

                /*Set date of starttime to day of schedule
                int intTodayDayOfYear = datNewDate.DayOfYear;
                int intScheduleDayOfYear = schedule.ScheduleDate.DayOfYear;

                int DifferenceDays = intScheduleDayOfYear - intTodayDayOfYear;
                TimeSpan Correction = new TimeSpan(DifferenceDays, 0, 0, 0);*/

                DateTime newStartTime = datNewDate;
                DateTime newEndTime = newStartTime.AddMinutes(showtime.Movie.Runtime);

                if (newStartTime < EarliestTime || newEndTime > LatestTime)
                {
                    ViewBag.TimeError = "The start time must be after 9:00am and the last movie must end by midnight.";
                    return View(showtime);
                }


                //not during another movie in same theater (including buffer)
                DateTime BufferBefore = newStartTime.AddMinutes(-25);
                DateTime BufferAfter = newEndTime.AddMinutes(25);

                var query = from showt in newSchedule.Showtimes
                            select showt;

                query = query.Where(showt => showt.ShowtimeID != newshowtime.ShowtimeID);
                query = query.Where(showt => showt.Theater == SelectedTheater);

                query = query.Where(showt => (showt.StartTime > BufferBefore && showt.StartTime < BufferAfter) || (showt.EndTime > BufferBefore && showt.EndTime < BufferAfter) || (showt.StartTime < BufferBefore && showt.EndTime > BufferAfter));

                List<Showtime> ConflictingShowtimes = query.ToList();
                if (ConflictingShowtimes.Count() != 0)
                {
                    ViewBag.TimeError = "This showtime conflicts with another existing one.";
                    return View(showtime);
                }

                //same movie not playing at same time in other theater
                var qy = from stime in newSchedule.Showtimes
                         select stime;

                qy = qy.Where(stime => stime.Theater != SelectedTheater);
                qy = qy.Where(stime => stime.Movie.Title == showtime.Movie.Title);
                qy = qy.Where(stime => (stime.StartTime >= newStartTime && stime.StartTime <= newEndTime) || (stime.EndTime >= newStartTime && stime.EndTime <= newEndTime));

                List<Showtime> SameMovies = qy.ToList();
                if (SameMovies.Count() != 0)
                {
                    ViewBag.TimeError = "This movie is already scheduled to play in the other theater during this time.";
                    return View(showtime);
                }
                foreach(Ticket tick in showtime.Tickets)
                {
                    AppUser AfflictedUser = db.Users.Find(tick.Order.AppUser.Id);
                    OrdersController.EmailMessaging.SendEmail(AfflictedUser.Email, "Showtime Rescheduled", "We are emailing you to inform you that one of the showtimes that you have a ticket to has been rescheduled. Check your orders to find the new date and time");
                }
                showtime.StartTime = newStartTime;
                showtime.EndTime = newEndTime;
                showtime.Schedule = newSchedule;

            }
            else
            {
                ViewBag.TimeError = "Please enter a start time";
            }



            showtime.Theater = SelectedTheater;
            showtime.SpecialEvent = newshowtime.SpecialEvent;


            if (ModelState.IsValid)
            {
                db.Entry(showtime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("RemoveFromSchedule", "Schedules", new { ScheduleID = oldschedule.ScheduleID });
            }
            return View(showtime);
        }

        // GET: Showtimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showtime showtime = db.Showtimes.Find(id);
            Schedule schedule = db.Schedules.Find(showtime.Schedule.ScheduleID);
            if (showtime == null)
            {
                return HttpNotFound();
            }
            if(showtime.Schedule.ScheduleDate < DateTime.Now)
            {
                ViewBag.PastShowtime = "You can't delete a showtime after it has begun.";
                return RedirectToAction("RemoveFromSchedule", "Schedules", new { id = schedule.ScheduleID });
            }
            return View(showtime);
        }

        // POST: Showtimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Showtime showtime = db.Showtimes.Find(id);
            Schedule schedule = db.Schedules.Find(showtime.Schedule.ScheduleID);
            List<Ticket> tickets = new List<Ticket>();

            foreach(Ticket tick in showtime.Tickets)
            {
                tickets.Add(tick);
            }

            foreach(Ticket ticket in tickets)
            {
                Ticket ticktotake = db.Tickets.Find(ticket.TicketID);
                if (ticket.Taken == true)
                {
                    //TODO: Send email message notifying showtime has been cancelled
                    AppUser afflicteduser = db.Users.Find(ticktotake.Order.AppUser.Id);
                    OrdersController.EmailMessaging.SendEmail(afflicteduser.Email, "Showtime Cancelled", "We're sorry, one of the showtimes which you had a ticket to has been cancelled. Your order has been refunded");
                    if (ticktotake.Order.PaymentMethod == PaymentMethod.PopcornPoints)
                    {
                        ticktotake.Order.AppUser.PopcornPoints += 100;
                    }
                }
                
                db.Tickets.Remove(ticktotake);
            }
            
            db.Entry(schedule).State = EntityState.Modified;
            db.Showtimes.Remove(showtime);
            db.SaveChanges();
            return RedirectToAction("RemoveFromSchedule", "Schedules", new { ScheduleID = schedule.ScheduleID });
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
