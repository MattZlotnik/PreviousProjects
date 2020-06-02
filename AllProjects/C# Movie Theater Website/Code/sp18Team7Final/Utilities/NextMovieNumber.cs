using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sp18Team7Final.DAL;

namespace sp18Team7Final.Utilities
{
    public class NextMovieNumber
    {
        public static Int32 GetNextMovieNumber()
        {
            AppDbContext db = new AppDbContext();

            Int32 intMinMovieNumber;
            Int32 intNextMovieNumber;

            if (db.Orders.Count() == 0)
            {
                intMinMovieNumber = 3000;
            }
            else
            {
                intMinMovieNumber = db.Movies.Max(m => m.MovieNumber);
            }

            intNextMovieNumber = intMinMovieNumber + 1;

            return intNextMovieNumber;
        }
    }
}