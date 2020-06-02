namespace sp18Team7Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pleasework : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CreditCardID = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(nullable: false),
                        Expiration = c.DateTime(nullable: false),
                        CVV = c.Int(nullable: false),
                        DisplayString = c.String(),
                        CardType = c.Int(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CreditCardID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Address = c.String(),
                        PopcornPoints = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderNumber = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        EmployeeMade = c.Boolean(nullable: false),
                        PaymentMethod = c.Int(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CancelledOrder = c.Boolean(nullable: false),
                        Gift = c.Boolean(nullable: false),
                        DiscountType = c.String(),
                        CompletedOrder = c.Boolean(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        Seat = c.String(),
                        Taken = c.Boolean(nullable: false),
                        PriceAtPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RecipientID = c.String(),
                        Order_OrderID = c.Int(),
                        Showtime_ShowtimeID = c.Int(),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .ForeignKey("dbo.Showtimes", t => t.Showtime_ShowtimeID)
                .Index(t => t.Order_OrderID)
                .Index(t => t.Showtime_ShowtimeID);
            
            CreateTable(
                "dbo.Showtimes",
                c => new
                    {
                        ShowtimeID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Theater = c.Int(nullable: false),
                        SpecialEvent = c.Boolean(nullable: false),
                        Movie_MovieID = c.Int(),
                        Schedule_ScheduleID = c.Int(),
                    })
                .PrimaryKey(t => t.ShowtimeID)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID)
                .ForeignKey("dbo.Schedules", t => t.Schedule_ScheduleID)
                .Index(t => t.Movie_MovieID)
                .Index(t => t.Schedule_ScheduleID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieID = c.Int(nullable: false, identity: true),
                        MovieNumber = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Tagline = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        MPAARating = c.Int(nullable: false),
                        Actors = c.String(nullable: false),
                        Runtime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        ReviewText = c.String(),
                        CustomerRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Approved = c.Boolean(nullable: false),
                        TotalVotes = c.Int(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                        Movie_MovieID = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Movie_MovieID);
            
            CreateTable(
                "dbo.ReviewVotes",
                c => new
                    {
                        ReviewVoteID = c.Int(nullable: false, identity: true),
                        UpOrDown = c.Int(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                        Review_ReviewID = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewVoteID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Reviews", t => t.Review_ReviewID)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Review_ReviewID);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleID = c.Int(nullable: false, identity: true),
                        Completed = c.Boolean(nullable: false),
                        ScheduleDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleID);
            
            CreateTable(
                "dbo.PriceAndDiscounts",
                c => new
                    {
                        PriceAndDiscountID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PriceAndDiscountID);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_GenreID = c.Int(nullable: false),
                        Movie_MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Movie_MovieID })
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID, cascadeDelete: true)
                .Index(t => t.Genre_GenreID)
                .Index(t => t.Movie_MovieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "Showtime_ShowtimeID", "dbo.Showtimes");
            DropForeignKey("dbo.Showtimes", "Schedule_ScheduleID", "dbo.Schedules");
            DropForeignKey("dbo.Showtimes", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.ReviewVotes", "Review_ReviewID", "dbo.Reviews");
            DropForeignKey("dbo.ReviewVotes", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.Reviews", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GenreMovies", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.Tickets", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CreditCards", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GenreMovies", new[] { "Movie_MovieID" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_GenreID" });
            DropIndex("dbo.ReviewVotes", new[] { "Review_ReviewID" });
            DropIndex("dbo.ReviewVotes", new[] { "AppUser_Id" });
            DropIndex("dbo.Reviews", new[] { "Movie_MovieID" });
            DropIndex("dbo.Reviews", new[] { "AppUser_Id" });
            DropIndex("dbo.Showtimes", new[] { "Schedule_ScheduleID" });
            DropIndex("dbo.Showtimes", new[] { "Movie_MovieID" });
            DropIndex("dbo.Tickets", new[] { "Showtime_ShowtimeID" });
            DropIndex("dbo.Tickets", new[] { "Order_OrderID" });
            DropIndex("dbo.Orders", new[] { "AppUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CreditCards", new[] { "AppUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.GenreMovies");
            DropTable("dbo.PriceAndDiscounts");
            DropTable("dbo.Schedules");
            DropTable("dbo.ReviewVotes");
            DropTable("dbo.Reviews");
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
            DropTable("dbo.Showtimes");
            DropTable("dbo.Tickets");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CreditCards");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
