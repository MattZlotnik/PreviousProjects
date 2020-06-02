using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sp18Team7Final.Models
{
    public enum UpOrDown { Up, Down }

    public class ReviewVote
    {
        public Int32 ReviewVoteID { get; set; }

        [Required]
        [Display(Name = "Up or Down")]
        public UpOrDown UpOrDown { get; set; }

        // navigational properties
        public virtual AppUser AppUser { get; set; }

        public virtual Review Review { get; set; }
    }
}
