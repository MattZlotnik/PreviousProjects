using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sp18Team7Final.Models
{
    public class Review
    {
        public Int32 ReviewID { get; set; }

        [Display(Name = "Review Text")]
        public String ReviewText { get; set; }

        [Display(Name = "Customer Rating")]
        public Decimal CustomerRating { get; set; }

        [Display(Name = "Approved")]
        public Boolean Approved { get; set; }

        [Display(Name = "Total Votes")]
        public Int32 TotalVotes { get; set; }

        // navigation properties
        public virtual Movie Movie { get; set; }

        public virtual AppUser AppUser { get; set; }

        public virtual List<ReviewVote> ReviewVotes { get; set; }

        // if some navigational properties not specified
        public Review()
        {
            if (ReviewVotes == null)
            {
                ReviewVotes = new List<ReviewVote>();
            }
            if (Movie == null)
            {
                Movie = new Movie();
            }
        }
    }
}
