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
using sp18Team7Final.Utilities;

namespace sp18Team7Final.Controllers
{
    //TODO: work on ordering by start time
    public class SchedulesController : Controller
    {
        private AppDbContext db = new AppDbContext();
        //TODO: orderby showtimes when submitting schedule
        // GET: Schedules
        public ActionResult Index()
        {
            var query = from sch in db.Schedules
                        select sch;
            DateTime Today = DateTime.Now.Date;
            query = query.Where(sch => sch.ScheduleDate == Today);
            List<Schedule> SchedulesToDisplay = query.ToList();
            ViewBag.DateMessage = Today.ToString("MM/dd/yyyy");
            if (SchedulesToDisplay.Count == 0)
            {
                ViewBag.NoShowTime = "We're sorry, the schedule for this date has not been released";
            }
            return View(SchedulesToDisplay);
        }

        public ActionResult AllSchedules()
        {
            return View(db.Schedules.ToList());
        }

        // GET: Schedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedules/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduleID,ScheduleDate")] Schedule schedule)
        {
            var query = from sch in db.Schedules
                        select sch;

            query = query.Where(sch => sch.ScheduleDate == schedule.ScheduleDate);
            List<Schedule> SelectedSchedules = query.ToList();
            if (SelectedSchedules.Count() != 0)
            {
                ViewBag.DateError = "There is already a schedule on this date";
                return View(schedule);
            }

            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return View("Edit", schedule);
            }

            return View(schedule);
        }

        // GET: Schedules/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduleID,Completed,ScheduleDate")] Schedule sc)
        {
            Schedule schedule = db.Schedules.Find(sc.ScheduleID);
            if (sc.Completed == true)
            {
                // 45 minutes
                var qry = from st in schedule.Showtimes
                          select st;

                qry = qry.OrderBy(st => st.StartTime);
                var qry1 = qry.Where(st => st.Theater == 1);
                var qry2 = qry.Where(st => st.Theater == 2);

                List<Showtime> Theater1St = qry1.ToList();
                List<Showtime> Theater2St = qry2.ToList();

                int i = 0;
                while (i < (Theater1St.Count() - 1))
                {
                    if (Theater1St[i].EndTime.AddMinutes(45) < Theater1St[i + 1].StartTime)
                    {
                        ViewBag.DifferenceError = "You must schedule a movie every 45 minutes, at the minimum.";
                        return View(schedule);
                    }
                    i += 1;
                }

                int j = 0;
                while (j < (Theater2St.Count() - 1))
                {
                    if (Theater2St[j].EndTime.AddMinutes(45) < Theater2St[j + 1].StartTime)
                    {
                        ViewBag.DifferenceError = "You must schedule a movie every 45 minutes, at the minimum.";
                        return View(schedule);
                    }
                    j += 1;
                }


                // have a movie ending past 9:30pm
                var query = from st in schedule.Showtimes
                            select st;

                int RelevantHours = schedule.ScheduleDate.Hour;
                TimeSpan Comparison = new TimeSpan(RelevantHours, 0, 0);
                DateTime ThisDate = schedule.ScheduleDate.Subtract(Comparison);
                DateTime LastTime = ThisDate.AddHours(21);
                LastTime = LastTime.AddMinutes(30);
                DateTime FirstTime = ThisDate.AddHours(10);

                var query1 = query.Where(st => st.EndTime > LastTime && st.Theater == 1);
                var query2 = query.Where(st => st.EndTime > LastTime && st.Theater == 2);

                List<Showtime> Theater1Showtimes = query1.ToList();
                List<Showtime> Theater2Showtimes = query2.ToList();

                if (Theater1Showtimes.Count() < 1 || Theater2Showtimes.Count() < 1)
                {
                    ViewBag.EndError = "You must schedule a movie that ends past 9:30pm in both theaters.";
                    return View(schedule);
                }

                var query3 = query.Where(st => st.StartTime <= FirstTime && st.Theater == 1);
                var query4 = query.Where(st => st.StartTime <= FirstTime && st.Theater == 2);

                List<Showtime> Theater1EarlyTimes = query3.ToList();
                List<Showtime> Theater2EarlyTimes = query4.ToList();

                if(Theater1EarlyTimes.Count() < 1 || Theater2EarlyTimes.Count() < 1)
                {
                    ViewBag.StartError = "You must schedule a showtime before 10AM";
                        return View(schedule);
                }
                schedule.Completed = true;
            }
            
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllSchedules");
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult AddToSchedule(int ScheduleID)
        {
            Showtime st = new Showtime();

            Schedule schedule = db.Schedules.Find(ScheduleID);

            st.Schedule = schedule;

            return View(st);
        }

        [HttpPost]
        public ActionResult AddToSchedule(Showtime st, DateTime? datSelectedTime, int? SelectedTheater, Boolean SpecialEvent, String MovieTitle)
        {
            Schedule schedule = db.Schedules.Find(st.Schedule.ScheduleID);
            //TODO: ask katie if one movie at night makes a valid schedule/what time do movies have to start !!IT IS VALID!!

            if (MovieTitle == null)
            {
                ViewBag.MovieTitleError = "Please enter a movie title";
            }
            if (MovieTitle != null && MovieTitle != "")
            {
                var query = from m in db.Movies
                            select m;
                query = query.Where(m => m.Title == MovieTitle);
                List<Movie> SelectedMovies = query.ToList();
                if (SelectedMovies.Count == 0)
                {
                    ViewBag.MovieTitleError = "This movie does not exist";
                    return View("AddToSchedule", st);
                }
                else
                {
                    Movie SelectedMovie = SelectedMovies.First();
                    st.Movie = SelectedMovie;
                }
            }

            if (SelectedTheater == null)
            {
                ViewBag.TheaterError = "Please select a theater";
            }
            else
            {
                int RealSelectedTheater = SelectedTheater ?? 1;
                st.Theater = RealSelectedTheater;
            }

            // Time
            if (datSelectedTime != null)
            {
                DateTime datStartTime = datSelectedTime ?? new DateTime(1900, 1, 1);
                int RelevantHours = schedule.ScheduleDate.Hour;
                TimeSpan Comparison = new TimeSpan(RelevantHours, 0, 0);
                DateTime ThisDate = schedule.ScheduleDate.Subtract(Comparison);

                //after 9am and end
                DateTime EarliestTime = ThisDate.AddHours(9);
                DateTime LatestTime = ThisDate.AddHours(24);

                //Set date of starttime to day of schedule
                int intTodayDayOfYear = datStartTime.DayOfYear;
                int intScheduleDayOfYear = schedule.ScheduleDate.DayOfYear;

                int DifferenceDays = intScheduleDayOfYear - intTodayDayOfYear;
                TimeSpan Correction = new TimeSpan(DifferenceDays, 0, 0, 0);

                st.StartTime = datStartTime.Add(Correction);
                st.EndTime = st.StartTime.AddMinutes(st.Movie.Runtime);
                
                if (st.StartTime < EarliestTime || st.EndTime > LatestTime)
                {
                    ViewBag.TimeError = "The start time must be after 9:00am and the last movie must end by midnight.";
                    return View(st);
                }


                //not during another movie in same theater (including buffer)
                DateTime BufferBefore = st.StartTime.AddMinutes(-25);
                DateTime BufferAfter = st.EndTime.AddMinutes(25);

                var query = from showt in db.Showtimes
                            select showt;

                query = query.Where(showt => showt.Theater == st.Theater);

                query = query.Where(showt => (showt.StartTime > BufferBefore && showt.StartTime < BufferAfter) || (showt.EndTime > BufferBefore && showt.EndTime < BufferAfter) || (showt.StartTime < BufferBefore && showt.EndTime > BufferAfter));

                List<Showtime> ConflictingShowtimes = query.ToList();
                if (ConflictingShowtimes.Count() != 0)
                {
                    ViewBag.TimeError = "This showtime conflicts with another existing one.";
                    return View(st);
                }

                //same movie not playing at same time in other theater
                var qy = from stime in db.Showtimes
                         select stime;

                qy = qy.Where(stime => stime.Theater != st.Theater);
                qy = qy.Where(stime => stime.Movie.Title == st.Movie.Title);
                qy = qy.Where(stime => (stime.StartTime >= st.StartTime && stime.StartTime <= st.EndTime) || (stime.EndTime >= st.StartTime && stime.EndTime <= st.EndTime));

                List<Showtime> SameMovies = qy.ToList();
                if (SameMovies.Count() != 0)
                {
                    ViewBag.TimeError = "This movie is already scheduled to play in the other theater during this time.";
                    return View(st);
                }

                
            }
            else
            {
                ViewBag.TimeError = "Please enter a start time";
            }

            st.SpecialEvent = SpecialEvent;

            if(ViewBag.TheaterError != null || ViewBag.TimeError != null || ViewBag.MovieTitleError != null)
            {
                return View("AddToSchedule", st);
            }
            else
            {
                st.Schedule = schedule;
                schedule.Showtimes.Add(st);
                schedule.Showtimes.OrderBy(sch => sch.StartTime);
                db.Showtimes.Add(st);
                Utilities.CreateSeats.InstantiateSeats(st);
                db.SaveChanges();
                return RedirectToAction("Edit", "Schedules", new { id = schedule.ScheduleID });
            }
        }

        public ActionResult ScheduleSearch(DateTime? datSelectedDate)
        {
            var query = from sch in db.Schedules
                        select sch;

            if (datSelectedDate != null)
            {
                DateTime datSelectedDaterino = datSelectedDate ?? new DateTime(1900, 1, 1);
                query = query.Where(sch => sch.ScheduleDate == datSelectedDate);
                //TODO: only if not manager
                query = query.Where(sch => sch.Completed == true);
                DateTime datSelectedDay = datSelectedDaterino.Date;
                ViewBag.DateMessage = datSelectedDay.ToString("MM/dd/yyyy");
            }

            List<Schedule> SchedulesToDisplay = query.ToList();

            if (SchedulesToDisplay.Count == 0)
            {
                ViewBag.NoShowTime = "We're sorry, the schedule for this date has not been released";
            }
            return View("Index", SchedulesToDisplay);
        }

        public ActionResult RemoveFromSchedule(int ScheduleID)
        {
            Schedule schedule = db.Schedules.Find(ScheduleID);
            var query = from st in schedule.Showtimes
                        select st;
            List<Showtime> ShowtimesToDisplay = query.ToList();
            return View(ShowtimesToDisplay);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult CopySchedule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult CopySchedule(int? id, DateTime? datSchedDate, String SelectedTheater, String TargetTheater)
        {
            Schedule OldSchedule = db.Schedules.Find(id);
            
            if (datSchedDate == null)
            {
                ViewBag.EntryError = "You must select the date which you would like to copy this schedule to.";
                return View(OldSchedule);
            }
            DateTime datScheduleDate = datSchedDate ?? new DateTime(1900, 1, 1);
            if (datScheduleDate < DateTime.Now)
            {
                ViewBag.EntryError = "You can't insert copied schedules into the past";
                return View(OldSchedule);
            }

            if (SelectedTheater == null)
            {
                ViewBag.EntryError = "You must select the theater's schedule which you would like to copy.";
                return View(OldSchedule);
            }

            if (TargetTheater == null)
            {
                ViewBag.EntryError = "You must select the theater which you would like to insert this schedule into.";
                return View(OldSchedule);
            }

            var query = db.Schedules.Where(s => s.ScheduleDate == datScheduleDate);
            List<Schedule> TheSchedule = query.ToList();
            Schedule NewSchedule = new Schedule();
            if (TheSchedule.Count() != 0)
            {
                Schedule Sched = TheSchedule.First();

                var qy = from shot in Sched.Showtimes
                         select shot;

                int intTargetTheater = Convert.ToInt32(TargetTheater);
                qy = qy.Where(shot => shot.Theater == intTargetTheater);
                List<Showtime> Conflicts = qy.ToList();

                if (Conflicts.Count() != 0)
                {
                    ViewBag.EntryError = "You can't copy this schedule to that theater on this day, because there are already showtimes scheduled in that theater on that day.";
                    return View(OldSchedule);
                }

                NewSchedule = TheSchedule.First();
            }
            else
            {
                NewSchedule = new Schedule();
            }

            List<Showtime> ShowtimeCopies = new List<Showtime>();
            int Correction = Math.Abs(OldSchedule.ScheduleDate.DayOfYear - datScheduleDate.DayOfYear);

            if (TargetTheater == "1")
            { 
                foreach(Showtime st in OldSchedule.Showtimes.Where(st => st.Theater == Convert.ToInt32(SelectedTheater)))
                {
                    Showtime RealShowtime = new Showtime();
                    RealShowtime.Movie = st.Movie;
                    RealShowtime.SpecialEvent = st.SpecialEvent;
                    RealShowtime.StartTime = st.StartTime.AddDays(Correction);
                    RealShowtime.EndTime = RealShowtime.StartTime.AddMinutes(st.Movie.Runtime);
                    RealShowtime.Theater = 1;
                    Utilities.CreateSeats.InstantiateSeats(RealShowtime);
                    ShowtimeCopies.Add(RealShowtime);
                    db.Showtimes.Add(RealShowtime);
                }
            }
            if (TargetTheater == "2")
            {
                foreach (Showtime st in OldSchedule.Showtimes.Where(st => st.Theater == Convert.ToInt32(SelectedTheater)))
                {
                    Showtime RealShowtime = new Showtime();
                    RealShowtime.Movie = st.Movie;
                    RealShowtime.SpecialEvent = st.SpecialEvent;
                    RealShowtime.StartTime = st.StartTime.AddDays(Correction);
                    RealShowtime.EndTime = RealShowtime.StartTime.AddMinutes(st.Movie.Runtime);
                    RealShowtime.Theater = 2;
                    Utilities.CreateSeats.InstantiateSeats(RealShowtime);
                    ShowtimeCopies.Add(RealShowtime);
                    db.Showtimes.Add(RealShowtime);
                }
            }
            if (TheSchedule.Count() == 0)
            {
                NewSchedule.Completed = false;
                NewSchedule.ScheduleDate = datScheduleDate;
                db.Schedules.Add(NewSchedule);
            }
            
            foreach(Showtime show in ShowtimeCopies)
            {
                NewSchedule.Showtimes.Add(show);
            }
            db.SaveChanges();

            return View("Details", NewSchedule);
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
