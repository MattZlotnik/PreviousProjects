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
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace sp18Team7Final.Utilities
{
    public class Pricing
    {

        public static List<String> GetTicketPrice(Order ordi, AppUser curruseri)
        {
            AppDbContext db = new AppDbContext();

            Order ord = db.Orders.Find(ordi.OrderID);
            ord.Subtotal = 0;
            AppUser curruser = db.Users.Find(curruseri.Id);
            foreach (Ticket tick in ord.Tickets)
            {
                if (tick.Showtime.StartTime.DayOfWeek >= DayOfWeek.Monday && tick.Showtime.StartTime.DayOfWeek <= DayOfWeek.Friday && tick.Showtime.StartTime.Hour < 12)
                {
                    var query = from pd in db.PriceAndDiscounts
                                select pd;
                    query = query.Where(pd => pd.Name == "Matinee");
                    List<PriceAndDiscount> Thisdiscountlist = query.ToList();
                    PriceAndDiscount thisdiscount = Thisdiscountlist.First();
                    tick.PriceAtPayment = thisdiscount.Amount;
                    ord.DiscountNames.Add(thisdiscount.Name);
                    db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else if (tick.Showtime.StartTime.DayOfWeek == DayOfWeek.Tuesday && tick.Showtime.StartTime.Hour < 17)
                {
                    var query = from pd in db.PriceAndDiscounts
                                select pd;
                    query = query.Where(pd => pd.Name == "Tuesday Discount");
                    List<PriceAndDiscount> Thisdiscountlist = query.ToList();
                    PriceAndDiscount thisdiscount = Thisdiscountlist.First();
                    tick.PriceAtPayment = thisdiscount.Amount;
                    ord.DiscountNames.Add(thisdiscount.Name + ", ");
                    db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else if (tick.Showtime.StartTime.DayOfWeek >= DayOfWeek.Monday && tick.Showtime.StartTime.DayOfWeek < DayOfWeek.Friday)
                {
                    var query = from pd in db.PriceAndDiscounts
                                select pd;
                    query = query.Where(pd => pd.Name == "Post Matinee MTWTh");
                    List<PriceAndDiscount> Thisdiscountlist = query.ToList();
                    PriceAndDiscount thisdiscount = Thisdiscountlist.First();
                    tick.PriceAtPayment = thisdiscount.Amount;
                    ord.DiscountNames.Add("");
                    db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else if (tick.Showtime.StartTime.DayOfWeek == DayOfWeek.Friday || tick.Showtime.StartTime.DayOfWeek == DayOfWeek.Saturday || tick.Showtime.StartTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    var query = from pd in db.PriceAndDiscounts
                                select pd;
                    query = query.Where(pd => pd.Name == "Weekends");
                    List<PriceAndDiscount> Thisdiscountlist = query.ToList();
                    PriceAndDiscount thisdiscount = Thisdiscountlist.First();
                    tick.PriceAtPayment = thisdiscount.Amount;
                    ord.DiscountNames.Add("");
                    db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                if (tick.Showtime.SpecialEvent == false)
                {
                    if (DateTime.Now < tick.Showtime.StartTime.AddDays(-2))
                    {
                        var query = from pd in db.PriceAndDiscounts
                                    select pd;
                        query = query.Where(pd => pd.Name == "48 Hours Prior");
                        List<PriceAndDiscount> Thisdiscountlist = query.ToList();
                        PriceAndDiscount thisdiscount = Thisdiscountlist.First();
                        tick.PriceAtPayment += thisdiscount.Amount;
                        ord.DiscountNames.Add(thisdiscount.Name + ", ");
                        db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    if (curruser.Birthday.AddYears(60) < DateTime.Today)
                    {
                        var query = from pd in db.PriceAndDiscounts
                                    select pd;
                        query = query.Where(pd => pd.Name == "Senior Citizen");
                        List<PriceAndDiscount> Thisnewdiscountlist = query.ToList();
                        PriceAndDiscount Thisnewdiscount = Thisnewdiscountlist.First();
                        tick.PriceAtPayment += Thisnewdiscount.Amount;
                        ord.DiscountNames.Add(Thisnewdiscount.Name + ", ");
                        db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                ord.Subtotal += tick.PriceAtPayment;
                db.Entry(tick).State = System.Data.Entity.EntityState.Modified;

            }

            
            db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return (ord.DiscountNames);
        }
    }
}