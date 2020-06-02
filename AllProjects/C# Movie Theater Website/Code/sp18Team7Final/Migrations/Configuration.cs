namespace sp18Team7Final.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using sp18Team7Final.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<sp18Team7Final.DAL.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(sp18Team7Final.DAL.AppDbContext context)
        {
            GenreData AddGenres = new GenreData();
            AddGenres.SeedGenres(context);

            MovieData AddMovies = new MovieData();
            AddMovies.SeedMovies(context);

            Customers AddCustomers = new Customers();
            AddCustomers.SeedCustomers(context);

            Employees AddEmployees = new Employees();
            AddEmployees.SeedEmployees(context);

            MovieSchedule SeedShowtimes = new MovieSchedule();
            SeedShowtimes.SeedShowtimes(context);

            PricingData SeedPrices = new PricingData();
            SeedPrices.SeedPrices(context);
        }
    }
}
