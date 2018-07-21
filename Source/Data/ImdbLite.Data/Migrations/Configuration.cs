namespace ImdbLite.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using ImdbLite.Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ImdbLite.Common;


    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            PopulateRoles(context);
            context.SaveChanges();
            //PopulateCities(context);
            //base.Seed(context);
        }

        private static void PopulateRoles(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdminRole));
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.UserRole));
        }

        private static void PopulateGenres(ApplicationDbContext context)
        {
            var genres = new List<Genre>()
            {
                new Genre {Name = "Action",CreatedOn=DateTime.Now},
                new Genre{Name = "Adventure",CreatedOn=DateTime.Now},
                new Genre{Name = "Animation",CreatedOn=DateTime.Now},
                new Genre{Name = "Biography",CreatedOn=DateTime.Now},
                new Genre{Name = "Comedy",CreatedOn=DateTime.Now},
                new Genre{Name = "Crime",CreatedOn=DateTime.Now},
                new Genre{Name = "Documentary",CreatedOn=DateTime.Now},
                new Genre{Name = "Drama",CreatedOn=DateTime.Now},
                new Genre{Name = "Family",CreatedOn=DateTime.Now},
                new Genre{Name = "Fantasy",CreatedOn=DateTime.Now},
                new Genre{Name = "FilmNoir",CreatedOn=DateTime.Now},
                new Genre{Name = "Horror",CreatedOn=DateTime.Now},
                new Genre{Name = "History",CreatedOn=DateTime.Now},
                new Genre{Name = "Music",CreatedOn=DateTime.Now},
                new Genre{Name = "Musical",CreatedOn=DateTime.Now},
                new Genre{Name = "Mystery",CreatedOn=DateTime.Now},
                new Genre{Name = "Romance",CreatedOn=DateTime.Now},
                new Genre{Name = "SciFi",CreatedOn=DateTime.Now},
                new Genre{Name = "Short",CreatedOn=DateTime.Now},
                new Genre{Name = "Sport",CreatedOn=DateTime.Now},
                new Genre{Name = "Thriller",CreatedOn=DateTime.Now},
                new Genre{Name = "War",CreatedOn=DateTime.Now},
                new Genre{Name = "Western",CreatedOn=DateTime.Now},

            };

            genres.ForEach(g => context.Genres.AddOrUpdate(p => p.Name, g));
        }

        private static void PopulateCities(ApplicationDbContext context)
        {
            var cities = new List<City>()
            {
                new City{Name="Burgas",CreatedOn=DateTime.Now},
                new City{Name="Sofia",CreatedOn=DateTime.Now},
                new City{Name="Plovdiv",CreatedOn=DateTime.Now},
                new City{Name="Varna",CreatedOn=DateTime.Now},
                new City{Name="Stara Zagora",CreatedOn=DateTime.Now},
                new City{Name="Ruse",CreatedOn=DateTime.Now},
                new City{Name="Pleven",CreatedOn=DateTime.Now},
                new City{Name="Sliven",CreatedOn=DateTime.Now}
            };

            cities.ForEach(g => context.Cities.AddOrUpdate(p => p.Name, g));
        }
    }
}
