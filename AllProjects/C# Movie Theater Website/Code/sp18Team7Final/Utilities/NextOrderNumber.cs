using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sp18Team7Final.DAL;

namespace sp18Team7Final.Utilities
{
    public class NextOrderNumber
    {
        public static Int32 GetNextOrderNumber()
        {
            AppDbContext db = new AppDbContext();

            Int32 intMinOrderNumber;
            Int32 intNextOrderNumber;

            if (db.Orders.Count() == 0)
            {
                intMinOrderNumber = 10000;
            }
            else
            {
                intMinOrderNumber = db.Orders.Max(p => p.OrderNumber);
            }

            intNextOrderNumber = intMinOrderNumber + 1;

            return intNextOrderNumber;
        }
    }
}