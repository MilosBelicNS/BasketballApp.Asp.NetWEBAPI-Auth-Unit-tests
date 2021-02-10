namespace BasketballApp.Asp.NetWebApi.Migrations
{
    using BasketballApp.Asp.NetWebApi.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BasketballApp.Asp.NetWebApi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BasketballApp.Asp.NetWebApi.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            context.Clubs.AddOrUpdate(
                new Club() { Name = "Sacramento Kings", League = "NBA", Founded = 1985, Trophies = 5 },
                new Club() { Name = "Dallas Mavericks", League = "NBA", Founded = 1980, Trophies = 6 },
                new Club() { Name = "Utah Jazz", League = "NBA", Founded = 1974, Trophies = 0 });

            context.SaveChanges();

            context.Players.AddOrUpdate(
                new Player() { Name = "Glenn Robinson III", Matches = 62, Born = 1994, PointsAverage = 11.7m, ClubId = 1 },
                new Player() { Name = "Luka Doncic", Matches = 26, Born = 1999, PointsAverage = 18.2m, ClubId = 2 },
                new Player() { Name = "Bojan Bogdanovic", Matches = 105, Born = 1989, PointsAverage = 14.8m, ClubId = 3 },
                new Player() { Name = "Nemanja Bjelica", Matches = 25, Born = 1988, PointsAverage = 10.8m, ClubId = 1 });

            context.SaveChanges();
        }
    }
}
