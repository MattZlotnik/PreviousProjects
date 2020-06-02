using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sp18Team7Final.Models;

namespace sp18Team7Final.Utilities
{
    public class Eligibility
    {
        public static bool ViewerEligibility(AppUser user, Showtime showtime)
        {
            bool eligible = true;

            if (showtime.Movie.MPAARating == MPAARating.R || showtime.Movie.MPAARating == MPAARating.NC17)
            {
                if (user.Birthday.AddYears(18) > DateTime.Today)
                {
                    eligible = false;
                }
            }

            return eligible;
        }
    }
}