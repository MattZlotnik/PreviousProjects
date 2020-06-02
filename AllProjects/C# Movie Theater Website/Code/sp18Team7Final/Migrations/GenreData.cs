using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sp18Team7Final.Models;
using sp18Team7Final.Migrations;
using sp18Team7Final.DAL;
using System.Data.Entity.Migrations;

namespace sp18Team7Final.Migrations
{
    public class GenreData
    {
        public void SeedGenres(AppDbContext db)
        {
            Genre g1 = new Genre();
            g1.GenreName = "Action";
            db.Genres.AddOrUpdate(g => g.GenreName, g1);
            db.SaveChanges();

            Genre g2 = new Genre();
            g2.GenreName = "Adventure";
            db.Genres.AddOrUpdate(g => g.GenreName, g2);
            db.SaveChanges();

            Genre g3 = new Genre();
            g3.GenreName = "Animation";
            db.Genres.AddOrUpdate(g => g.GenreName, g3);
            db.SaveChanges();

            Genre g4 = new Genre();
            g4.GenreName = "Comedy";
            db.Genres.AddOrUpdate(g => g.GenreName, g4);
            db.SaveChanges();

            Genre g5 = new Genre();
            g5.GenreName = "Crime";
            db.Genres.AddOrUpdate(g => g.GenreName, g5);
            db.SaveChanges();

            Genre g6 = new Genre();
            g6.GenreName = "Drama";
            db.Genres.AddOrUpdate(g => g.GenreName, g6);
            db.SaveChanges();

            Genre g7 = new Genre();
            g7.GenreName = "Family";
            db.Genres.AddOrUpdate(g => g.GenreName, g7);
            db.SaveChanges();

            Genre g8 = new Genre();
            g8.GenreName = "Fantasy";
            db.Genres.AddOrUpdate(g => g.GenreName, g8);
            db.SaveChanges();

            Genre g9 = new Genre();
            g9.GenreName = "History";
            db.Genres.AddOrUpdate(g => g.GenreName, g9);
            db.SaveChanges();

            Genre g10 = new Genre();
            g10.GenreName = "Horror";
            db.Genres.AddOrUpdate(g => g.GenreName, g10);
            db.SaveChanges();

            Genre g11 = new Genre();
            g11.GenreName = "Musical";
            db.Genres.AddOrUpdate(g => g.GenreName, g11);
            db.SaveChanges();

            Genre g12 = new Genre();
            g12.GenreName = "Romance";
            db.Genres.AddOrUpdate(g => g.GenreName, g12);
            db.SaveChanges();

            Genre g13 = new Genre();
            g13.GenreName = "Science Fiction";
            db.Genres.AddOrUpdate(g => g.GenreName, g13);
            db.SaveChanges();

            Genre g14 = new Genre();
            g14.GenreName = "Thriller";
            db.Genres.AddOrUpdate(g => g.GenreName, g14);
            db.SaveChanges();

            Genre g15 = new Genre();
            g15.GenreName = "War";
            db.Genres.AddOrUpdate(g => g.GenreName, g15);
            db.SaveChanges();

            Genre g16 = new Genre();
            g16.GenreName = "Western";
            db.Genres.AddOrUpdate(g => g.GenreName, g16);
            db.SaveChanges();
        }
    }
}