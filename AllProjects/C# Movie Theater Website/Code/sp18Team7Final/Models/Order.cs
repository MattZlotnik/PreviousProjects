using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sp18Team7Final.Models
{
    public enum PaymentMethod { Card, PopcornPoints }
    
    public class Order
    {
        const Decimal SALES_TAX = 0.0825m;

        public Int32 OrderID { get; set; }

        public Int32 OrderNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public Boolean EmployeeMade { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Subtotal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Subtotal { get; set; }

        public List<String> DiscountNames { get; set; }

        [Display(Name = "Tax")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Tax
        {
            get { return (Subtotal * SALES_TAX); }
        }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Total
        {
            get { return (Subtotal + Tax); }
        }

        [Display(Name = "Cancelled Order")]
        public Boolean CancelledOrder { get; set; }

        [Display(Name = "Gift")]
        public Boolean Gift { get; set; }

        //[Display(Name = "Recipient")]
        //public AppUser Recipient { get; set; }

        [Display(Name = "Discount Type")]
        public String DiscountType { get; set; }

        public Boolean CompletedOrder { get; set; }

        // navigational properties
        [Display(Name = "User")]
        public virtual AppUser AppUser { get; set; }

        public virtual List<Ticket> Tickets { get; set; }

        // if some navigational properties not selected
        public Order()
        {
            if (Tickets == null)
            {
                Tickets = new List<Ticket>();
            }
            if (DiscountNames == null)
            {
                DiscountNames = new List<string>();
            }
        }
    }
}
