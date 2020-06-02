using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace sp18Team7Final.Models
{
    public class Genre
    {
        public Int32 GenreID { get; set; }

        [Display(Name = "Genre Name")]
        public String GenreName { get; set; }

        public virtual List<Movie> Movies { get; set; }

        public Genre()
        {
            if(Movies == null)
            {
                Movies = new List<Movie>();
            }
        }

    }
}