using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sp18Team7Final.Models
{
    public enum CardType { Mastercard, Visa, Discover, Amex }

    public class CreditCard
    {
        public Int32 CreditCardID { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public String CardNumber { get; set; }

        [Required]
        [Display(Name = "Expiration Date")]
        public DateTime Expiration { get; set; }

        [Required]
        [Display(Name = "Security Code (CVV)")]
        public Int32 CVV { get; set; }

        //TODO: make method to create this in user
        public String DisplayString { get; set; }

        public CardType CardType { get; set; }

        // navigational properties
        public virtual AppUser AppUser { get; set; }
    }
}
