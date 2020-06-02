using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sp18Team7Final.Models
{
    public class Ticket
    {
        public Int32 TicketID { get; set; }


        [Display(Name = "Seat")]
        public String Seat { get; set; }  //String because "A2", "C4", "B1" are seats

        [Display(Name = "Taken")]
        public Boolean Taken { get; set; }

        [Display(Name = "Price at payment")]
        public Decimal PriceAtPayment { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public virtual Showtime Showtime { get; set; }

        //TODO: Remove
        public String RecipientID { get; set; }

        public virtual Order Order { get; set; }

    }
}
