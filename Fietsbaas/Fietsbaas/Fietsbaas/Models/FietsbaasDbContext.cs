using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Fietsbaas.Models
{
    public class FietsbaasDbContext : DbContext
    {
        public DbSet<Cyclist> Cyclists { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Racer> Racers { get; set; }
        public DbSet<RacerStageProgress> RacerStageProgress { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        public FietsbaasDbContext()
        {
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            const string ConnectionString = "Data Source=:memory:";
            //const string ConnectionString = "Data Source=Application.db;Cache=Shared

            optionsBuilder
                //.UseSqlite( ConnectionString )
                .UseInMemoryDatabase( nameof(FietsbaasDbContext ))
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }

        internal static void DropAndSeed()
        {
            using ( var context = new FietsbaasDbContext() )
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Create users
                var adminUser = new User()
                {
                    Email = "admin@mail.com",
                    Name = "Admin",
                    Password = "admin",
                    Points = 0,
                    Role = Role.Admin,
                };
                context.Users.Add( adminUser );

                var testUser = new User()
                {
                    Email = "user@mail.com",
                    Name = "User",
                    Password = "user",
                    Points = 0,
                    Role = Role.User,
                };
                context.Users.Add( testUser );
                context.SaveChanges();

                // Create races
                var race1 = new Race()
                {
                    Name = "Tour de France 2022",
                    Description = "World's most famous cycling road race.",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays( 7 ),
                };
                context.Races.Add( race1 );

                var race2 = new Race()
                {
                    StartDate = new DateTime( 2022, 2, 14 ),
                    Name = "28th Valley of the Sun Stage Race",
                    Description = "God bless America",
                    EndDate = new DateTime( 2022, 2, 21 ),
                };
                context.Races.Add( race2 );
                context.SaveChanges();

                // Create stages
                var stage1 = new Stage()
                {
                    Name = "Etappe 1",
                    Race = race1,
                };
                context.Stages.Add( stage1 );

                var stage2 = new Stage()
                {
                    Name = "Etappe 2",
                    Race = race1,
                };
                context.Stages.Add( stage2 );

                var stage3 = new Stage()
                {
                    Name = "Stage 1",
                    Race = race2,
                };
                context.Stages.Add( stage3 );

                var stage4 = new Stage()
                {
                    Name = "Stage 2",
                    Race = race2,
                };
                context.Stages.Add( stage4 );
                context.SaveChanges();

                // Create cyclists
                var cyclist1 = new Cyclist()
                {
                    Name = "Jan Smit"
                };
                context.Cyclists.Add( cyclist1 );

                var cyclist2 = new Cyclist()
                {
                    Name = "Bert Jan"
                };
                context.Cyclists.Add( cyclist2 );
                context.SaveChanges();

                // Create racers
                var racer1 = new Racer()
                {
                    Cyclist = cyclist1,
                    Position = null,
                    Race = race1,
                    Status = RacerStatus.Active,
                };
                context.Racers.Add( racer1 );

                var racer2 = new Racer()
                {
                    Cyclist = cyclist1,
                    Position = 1,
                    Race = race1,
                    Status = RacerStatus.Finished,
                };
                context.Racers.Add( racer2 );
                context.SaveChanges();

                // Create teams
                var team1 = new Team()
                {
                    User = adminUser,
                    Race = race1,
                    Racers = new []
                    {
                        new TeamRacer()
                        {
                            IsReserve = false,
                            Racer = racer1,
                            Bet = BetType.WinsAnyRace,
                        }
                    }
                };
                context.Teams.Add( team1 );
                context.SaveChanges();
            }
        }
    }
}
