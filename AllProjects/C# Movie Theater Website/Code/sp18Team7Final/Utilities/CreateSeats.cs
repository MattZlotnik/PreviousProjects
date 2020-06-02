using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using sp18Team7Final.Models;
using sp18Team7Final.DAL;
using System.Web.Mvc;

namespace sp18Team7Final.Utilities
{
    public class CreateSeats
    {


        public static void InstantiateSeats(Showtime st)
        { 
            AppDbContext db = new AppDbContext();
            List<String> SeatNames = new List<String>();
            int i = 1;
            while(i<9)
            {
                SeatNames.Add("A" + Convert.ToString(i));
                SeatNames.Add("B" + Convert.ToString(i));
                SeatNames.Add("C" + Convert.ToString(i));
                SeatNames.Add("D" + Convert.ToString(i));
                i += 1;
            }

            foreach(String sn in SeatNames)
            {
                Ticket ticket = new Ticket();
                ticket.Seat = sn;
                ticket.Taken = false;
                
                ticket.Showtime = st; //st.Tickets.Add(tick);
                db.Tickets.Add(ticket);

                db.SaveChanges();
                
            }
        }
    }
}