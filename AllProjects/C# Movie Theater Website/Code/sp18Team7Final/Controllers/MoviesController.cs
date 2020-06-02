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


//TODO: Add Genres to Index
// Ask Katie "Can you only search for one actor at a time?" - no
//TODO: Search by runtime?

namespace sp18Team7Final.Controllers
{
    public enum BeforeAfter { Before, After };


    public class MoviesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Movies
        public ActionResult Index(String SearchString)
        {
            List<Movie> SelectedMovies = new List<Movie>();
            var query = from r in db.Movies
                        select r;
            if (SearchString != null)
            {
                //TODO: Figure out how much you can quicksearch by
                query = query.Where(m => m.Title.Contains(SearchString) || m.Tagline.Contains(SearchString));
            }

            SelectedMovies = query.ToList();
            ViewBag.TotalMovies = db.Movies.Count();
            ViewBag.SelectedMovies = SelectedMovies.Count();
            return View(db.Movies.ToList());
        }

        //Detailed Search Method
        public ActionResult DetailedSearch()
        {
            var genres = db.Genres.ToList();
            ViewBag.Genres = new MultiSelectList(genres, "GenreID", "GenreName");
            return View();
        }

        //TODO: DisplaySearchResults Method
        public ActionResult DisplaySearchResults(String SearchString, String ReleaseYearString, BeforeAfter BeforeAfter, MPAARating MPAARating, List<int> SelectedGenres, String RatingString, String ActorString)
        {
            // create query
            var query = from m in db.Movies
                        select m;

            // TODO: make sure genre search is working, what does this do??
            if (SelectedGenres != null)
            {
                List<Genre> SelGenres = new List<Genre>();
                foreach (Genre item in db.Genres)
                {
                    if (SelectedGenres.Contains(item.GenreID))
                    {
                        SelGenres.Add(item);
                    }
                }
            }
            



            // search conditions
            if (SearchString != null)
            {
                query = query.Where(m => m.Title.Contains(SearchString));
            }

            if (ReleaseYearString != null && ReleaseYearString != "")
            {
                try // ensure release year entry is valid
                {
                    Int32 intReleaseYear = Convert.ToInt32(ReleaseYearString);

                    if (BeforeAfter == BeforeAfter.After)
                    {
                        query = query.Where(m => m.ReleaseDate.Year >= intReleaseYear);
                    }

                    if (BeforeAfter == BeforeAfter.Before)
                    {
                        query = query.Where(m => m.ReleaseDate.Year <= intReleaseYear);
                    }
                }
                catch // invalid, display error
                {
                    ViewBag.ReleaseYearErrorMessage = "You must enter a valid number to search by Year";
                    //TODO: Create viewbag with all genres
                    return View("DetailedSearch");
                }
            }

            if (MPAARating != MPAARating.All)
            {
                if (MPAARating == MPAARating.G)
                {
                    query = query.Where(m => m.MPAARating == MPAARating.G);
                }
                if (MPAARating == MPAARating.PG)
                {
                    query = query.Where(m => m.MPAARating == MPAARating.PG);
                }
                if (MPAARating == MPAARating.PG13)
                {
                    query = query.Where(m => m.MPAARating == MPAARating.PG13);
                }
                if (MPAARating == MPAARating.R)
                {
                    query = query.Where(m => m.MPAARating == MPAARating.R);
                }
                if (MPAARating == MPAARating.NC17)
                {
                    query = query.Where(m => m.MPAARating == MPAARating.NC17);
                }
            }

            if (SelectedGenres != null)
            {
                if (SelectedGenres.Count != 0)
                {
                    query = query.Where(r => r.Genres.Any(x => SelectedGenres.Contains(x.GenreID)));
                }
            }
            

            if (RatingString != null && RatingString != "")
            {
                try // ensure rating entry is valid
                {
                    Decimal decRating = Convert.ToDecimal(RatingString);

                    query = query.Where(m => m.CustomerRatingAverage >= decRating);
                }
                catch // invalid, display error
                {
                    ViewBag.RatingErrorMessage = "You must enter a valid number to search by Rating";
                    //TODO: Create viewbag with all genres
                    return View("DetailedSearch");
                }
            }

            //TODO: Actor string detailed search will need to be found
            if (ActorString != null)
            {
                query = query.Where(m => m.Actors.Contains(ActorString));
            }

            List<Movie> MoviesToDisplay = new List<Movie>();
            MoviesToDisplay = query.ToList();

            MoviesToDisplay.OrderByDescending(m => m.CustomerRatingAverage);
            ViewBag.TotalMovies = db.Movies.Count();
            ViewBag.SelectedMovies = MoviesToDisplay.Count();

            return View("Index", MoviesToDisplay);
        }


        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            List<Genre> AllGenres = Utilities.GetAllGenres.GetTheGenres();
            SelectList AllTheGenres = new SelectList(AllGenres.OrderBy(gen => gen.GenreID), "GenreID", "GenreName");
            ViewBag.AllGenres = AllTheGenres;
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieID,Title,Tagline,ReleaseDate,MPAARating,CustomerRatingAverage,Actors,Runtime")] Movie movie, List<Int32> SelectedGenres)
        {
            List<Genre> AllGenres = Utilities.GetAllGenres.GetTheGenres();
            SelectList AllTheGenres = new SelectList(AllGenres.OrderBy(gen => gen.GenreID), "GenreID", "GenreName");
            ViewBag.AllGenres = AllTheGenres;

            if (SelectedGenres.Count() == 0)
            {
                ViewBag.CreationError = "Please select at least one Genre";
                return View(movie);
            }
            if(movie.MPAARating == MPAARating.All)
            {
                ViewBag.CreationError = "Please select an MPAA rating";
                return View(movie);
            }
            movie.MovieNumber = Utilities.NextMovieNumber.GetNextMovieNumber();
            if (ModelState.IsValid)
            {
                foreach(Int32 gen in SelectedGenres)
                {
                    Genre thisgen = db.Genres.Find(gen);
                    movie.Genres.Add(thisgen);
                }
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieID,Title,Tagline,ReleaseYear,MPAARating,CustomerRatingAverage,Actors,Runtime")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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
