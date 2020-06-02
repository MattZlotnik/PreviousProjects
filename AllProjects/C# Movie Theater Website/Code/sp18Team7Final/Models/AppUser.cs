using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

//TODO: Change this namespace to match your project
namespace sp18Team7Final.Models
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    //NOTE: This is the class for users
    public class AppUser : IdentityUser
    {
        //TODO: Put any additional fields that you need for your user here
        //First name is here as an example

        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Display(Name = "Birthday")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Address")]
        public String Address { get; set; }

        [Display(Name = "Popcorn Points")]
        public Int32 PopcornPoints { get; set; }

        public virtual List<CreditCard> CreditCards { get; set; }

        public virtual List<ReviewVote> ReviewVotes { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public virtual List<Order> Orders { get; set; }

        public AppUser()
        {
            if (CreditCards == null)
            {
                CreditCards = new List<CreditCard>();
            }

            if (ReviewVotes == null)
            {
                ReviewVotes = new List<ReviewVote>();
            }

            if (Reviews == null)
            {
                Reviews = new List<Review>();
            }

            if (Orders == null)
            {
                Orders = new List<Order>();
            }
        }


        //This method allows you to create a new user
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // NOTE: The authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}