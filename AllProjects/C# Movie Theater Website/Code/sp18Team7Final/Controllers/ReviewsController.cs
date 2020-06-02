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
    public class ReviewsController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private int movieID = new int();

        // GET: Reviews
        public ActionResult Index(int id)
        {
                Movie movie = db.Movies.Find(id);
                List<Review> MovieReviews = movie.Reviews.ToList();
                ViewBag.MovieID = id;
                var user = new AppUser();
                if(User.Identity.IsAuthenticated)
                {
                    String userID = User.Identity.GetUserId();
                    user = db.Users.First(x => x.Id == userID);
                    List<Order> AllOrders = user.Orders.ToList();
                    List<Ticket> UserTickets = new List<Ticket>();
                    List<Showtime> UserShowTimes = new List<Showtime>();
                    foreach (Order order in AllOrders)
                    {
                        UserTickets.Concat(order.Tickets);
                    }
                    foreach(Ticket ticket in UserTickets)
                    {
                        UserShowTimes.Add(ticket.Showtime);
                    }
                    if(UserShowTimes.Any(x=> x.Movie == movie))
                    {
                        ViewBag.DidUserPurchase = true;
                        ViewBag.MovieID = id;
                    }
                    if(User.IsInRole("Customer") == true)
                    {
                        MovieReviews = MovieReviews.FindAll(x => x.Approved == true);
                    }
                }
                return View(MovieReviews.OrderByDescending(r => r.TotalVotes));
        }

        public ActionResult IndexByUser(String id)
        {
            List<Review> UserReviews = db.Reviews.ToList();
            UserReviews = UserReviews.FindAll(x => x.AppUser.Id == id);
            return View("Index", UserReviews);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create(int id)
        {
            Movie MovieToReview = db.Movies.First(x => x.MovieID == id);
            ViewBag.Movie = MovieToReview.Title;
            ViewBag.MovieID = id;
            movieID = id;
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,ReviewText,CustomerRating,MovieID,Approved,TotalVotes")] Review review, int MovieID)
        {
            if (ModelState.IsValid)
            {
                review.CustomerRating = Convert.ToInt32(review.CustomerRating);
                String UserID = User.Identity.GetUserId();
                Movie MovieBeingReviewed = db.Movies.First(x => x.MovieID == MovieID);
                review.AppUser = db.Users.First(x => x.Id == UserID);
                review.Movie = MovieBeingReviewed;
                db.Reviews.Add(review);
                db.SaveChanges();
                IEnumerable<Review> MovieReviews = db.Reviews.ToList();
                MovieReviews = MovieReviews.Where(x => x.Movie.MovieID == MovieID);
                return View("Index", MovieReviews);
            }
            return View();
        }

        public ActionResult UpvoteReview(int reviewID, int movieID)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                TempData["msg"] = "<script>alert('You must be logged in to vote on a review.');</script>";
                return Redirect(Request.UrlReferrer.ToString());
            }
            String currentuserID = User.Identity.GetUserId();
            AppUser currentuser = db.Users.First(x => x.Id == currentuserID);
            IEnumerable<ReviewVote> reviewvotes = db.ReviewVotes.Where(x => x.Review.ReviewID == reviewID);
            IEnumerable<ReviewVote> UserReviews = reviewvotes.Where(x => x.AppUser == currentuser);
            if (UserReviews != null && UserReviews.GetEnumerator().MoveNext())
            {
                foreach (ReviewVote vote in UserReviews)
                {
                    if (vote.UpOrDown == UpOrDown.Down)
                    {
                        vote.UpOrDown = UpOrDown.Up;
                        db.Entry(vote).State = EntityState.Modified;
                        TempData["msg"] = "<script>alert('Your vote was changed.');</script>";
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('You have already upvoted this review.');</script>";
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
                db.SaveChanges();

            }
            else
            {
                Review MovieReview = db.Reviews.First(x => x.ReviewID == reviewID);
                IEnumerable<Review> MovieReviews = db.Reviews.Where(x => x.Movie.MovieID == movieID);
                Movie movie = db.Movies.First(x => x.MovieID == movieID);
                ReviewVote reviewvote = new ReviewVote();
                reviewvote.UpOrDown = UpOrDown.Up;
                reviewvote.AppUser = currentuser;
                reviewvote.Review = MovieReview;
                reviewvote.Review.Movie = movie;
                MovieReview.TotalVotes += 1;
                db.ReviewVotes.Add(reviewvote);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult DownvoteReview(int reviewID, int movieID)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                TempData["msg"] = "<script>alert('You must be logged in to vote on a review.');</script>";
                return Redirect(Request.UrlReferrer.ToString());
            }
            String currentuserID = User.Identity.GetUserId();
            AppUser currentuser = db.Users.First(x => x.Id == currentuserID);
            IEnumerable<ReviewVote> reviewvotes = db.ReviewVotes.Where(x => x.Review.ReviewID == reviewID);
            IEnumerable<ReviewVote> UserReviews = reviewvotes.Where(x => x.AppUser == currentuser);
            if (UserReviews != null && UserReviews.GetEnumerator().MoveNext())
            {
                foreach(ReviewVote vote in UserReviews)
                {
                    if (vote.UpOrDown == UpOrDown.Up)
                    {
                        vote.UpOrDown = UpOrDown.Down;
                        db.Entry(vote).State = EntityState.Modified;
                        TempData["msg"] = "<script>alert('Your vote was changed.');</script>";
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('You have already downvoted this review.');</script>";
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
                db.SaveChanges();

            }
            else
            {
                Review MovieReview = db.Reviews.First(x => x.ReviewID == reviewID);
                IEnumerable<Review> MovieReviews = db.Reviews.Where(x => x.Movie.MovieID == movieID);
                Movie movie = db.Movies.First(x => x.MovieID == movieID);
                ReviewVote reviewvote = new ReviewVote();
                reviewvote.UpOrDown = UpOrDown.Down;
                reviewvote.AppUser = currentuser;
                reviewvote.Review = MovieReview;
                reviewvote.Review.Movie = movie;
                MovieReview.TotalVotes += 1;
                db.ReviewVotes.Add(reviewvote);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,ReviewText,CustomerRating,Approved,TotalVotes")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Movies");
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
