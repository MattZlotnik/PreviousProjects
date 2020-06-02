using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sp18Team7Final.Models
{

    public class Showtime
    {
        public Int32 ShowtimeID { get; set; }

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:hh:mm}")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0:hh:mm}")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Theater")]
        public int Theater { get; set; }

        [Display(Name = "Special Event")]
        public Boolean SpecialEvent { get; set; }

        // navigational properties
        public virtual List<Ticket> Tickets { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Schedule Schedule { get; set; }

        // if some navigational properties not specified
        public Showtime()
        {
            if (Tickets == null)
            {
                Tickets = new List<Ticket>();
            }
        }
    }
}
