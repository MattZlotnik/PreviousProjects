using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sp18Team7Final.Models
{
    public class Schedule
    {
        public Int32 ScheduleID { get; set; }

        public Boolean Completed { get; set; }

        [Display(Name = "Schedule Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ScheduleDate { get; set; }

        public virtual List<Showtime> Showtimes { get; set; }

        public Schedule()
        {
            if (Showtimes == null)
            {
                Showtimes = new List<Showtime>();
            }
        }

    }
}