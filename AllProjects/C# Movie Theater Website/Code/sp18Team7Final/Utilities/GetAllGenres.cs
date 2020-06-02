using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sp18Team7Final.Models;
using sp18Team7Final.DAL;

namespace sp18Team7Final.Utilities
{
    public class GetAllGenres
    {
        

        public static List<Genre> GetTheGenres()
        {
            AppDbContext db = new AppDbContext();
            var query = from genre in db.Genres
                        select genre;
            List<Genre> AllGenres = query.ToList();
            return AllGenres;
        }
    }
}