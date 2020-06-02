using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

using sp18Team7Final.Models;


namespace sp18Team7Final.DAL
{
    // NOTE: Here's your db context for the project.  All of your db sets should go in here
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
            : base("MyDBConnection", throwIfV1Schema: false) { }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }


        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<ReviewVote> ReviewVotes { get; set; }

        public DbSet<Showtime> Showtimes { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<PriceAndDiscount> PriceAndDiscounts { get; set; }

        //NOTE: This is a dbSet that you need to make roles work
        public DbSet<AppRole> AppRoles { get; set; }

        public System.Data.Entity.DbSet<sp18Team7Final.Models.Schedule> Schedules { get; set; }

    }
}