using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sp18Team7Final.Models
{
    public enum MPAARating {All, G, PG, PG13, R, NC17, Unrated}

    public class Movie
    {
        
        public Int32 MovieID { get; set; }

        public Int32 MovieNumber { get; set; }

        [Required(ErrorMessage = "Please Enter a Title")]
        [Display(Name = "Title")]
        public String Title { get; set; }

        [Display(Name = "Tagline")]
        public String Tagline { get; set; }

        [Required(ErrorMessage = "Please Enter a Release Date")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please Enter an MPAA")]
        [Display(Name = "MPAA Rating")]
        public MPAARating MPAARating { get; set; }

        [Display(Name = "Customer Rating Average")]
        public Decimal CustomerRatingAverage
        {
            get { if (Reviews.Count == 0) { return 0; } else { return Reviews.Average(x => x.CustomerRating); } }
        }

        [Required(ErrorMessage = "Please Enter at least one Actor")]
        [Display(Name = "Actors")]
        public String Actors { get; set; }

        [Required(ErrorMessage = "Please Enter a Runtime")]
        [Display(Name = "Runtime")]
        public Int32 Runtime { get; set; }


        // navigational properties
        [Display(Name = "Genre")]
        public virtual List<Genre> Genres { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public virtual List<Showtime> Showtimes { get; set; }

        // if some navigational properties not specified
        public Movie()
        {
            if (Genres == null)
            {
                Genres = new List<Genre>();
            }

            if (Reviews == null)
            {
                Reviews = new List<Review>();
            }

            if (Showtimes == null)
            {
                Showtimes = new List<Showtime>();
            }
        }
    }
}
