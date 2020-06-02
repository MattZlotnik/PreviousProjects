using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sp18Team7Final.Models;
using sp18Team7Final.Migrations;
using sp18Team7Final.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;

namespace sp18Team7Final.Migrations
{
    public class PricingData
    {
        public void SeedPrices(AppDbContext db)
        {
            PriceAndDiscount p1 = new PriceAndDiscount();
            p1.Amount = 5;
            p1.Name = "Matinee";
            db.PriceAndDiscounts.AddOrUpdate(p => p.Name, p1);
            db.SaveChanges();

            PriceAndDiscount p2 = new PriceAndDiscount();
            p2.Amount = 8;
            p2.Name = "Tuesday Discount";
            db.PriceAndDiscounts.AddOrUpdate(p => p.Name, p2);
            db.SaveChanges();

            PriceAndDiscount p3 = new PriceAndDiscount();
            p3.Amount = -2;
            p3.Name = "Senior Citizen";
            db.PriceAndDiscounts.AddOrUpdate(p => p.Name, p3);
            db.SaveChanges();

            PriceAndDiscount p4 = new PriceAndDiscount();
            p4.Amount = 10;
            p4.Name = "Post Matinee MTWTh";
            db.PriceAndDiscounts.AddOrUpdate(p => p.Name, p4);
            db.SaveChanges();

            PriceAndDiscount p5 = new PriceAndDiscount();
            p5.Amount = 12;
            p5.Name = "Post Matinee Friday";
            db.PriceAndDiscounts.AddOrUpdate(p => p.Name, p5);
            db.SaveChanges();

            PriceAndDiscount p6 = new PriceAndDiscount();
            p6.Amount = 12;
            p6.Name = "Weekends";
            db.PriceAndDiscounts.AddOrUpdate(p => p.Name, p6);
            db.SaveChanges();

            PriceAndDiscount p7 = new PriceAndDiscount();
            p7.Amount = -1;
            p7.Name = "48 Hours Prior";
            db.PriceAndDiscounts.AddOrUpdate(p => p.Name, p7);
            db.SaveChanges();
        }
        
    }
}