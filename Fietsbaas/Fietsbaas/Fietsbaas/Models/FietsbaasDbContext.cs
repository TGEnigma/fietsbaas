using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fietsbaas.Services.SportRadar;
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
                    Password = "admin",
                    Points = 0,
                    Role = Role.Admin,
                };
                context.Users.Add( adminUser );

                var testUser = new User()
                {
                    Email = "user@mail.com",
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
                    Cyclist = cyclist2,
                    Position = 1,
                    Race = race1,
                    Status = RacerStatus.Finished,
                };
                context.Racers.Add( racer2 );

                var racer3 = new Racer()
                {
                    Cyclist = cyclist1,
                    Position = null,
                    Race = race2,
                    Status = RacerStatus.Active,
                };
                context.Racers.Add( racer3 );

                var racer4 = new Racer()
                {
                    Cyclist = cyclist2,
                    Position = 1,
                    Race = race2,
                    Status = RacerStatus.Finished,
                };
                context.Racers.Add( racer4 );

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

                var team2 = new Team()
                {
                    User = adminUser,
                    Race = race2,
                    Racers = new[]
                    {
                        new TeamRacer()
                        {
                            IsReserve = false,
                            Racer = racer1,
                            Bet = BetType.WinsAnyRace,
                        }
                    }
                };
                context.Teams.Add( team2 );

                context.SaveChanges();
            }
        }

        internal static async Task SeedWithSportradar()
        {
            var service = new SportRadarService();
            var seasons = await service.GetSeasonsAsync();
            using ( var context = new FietsbaasDbContext() )
            {
                foreach ( var season in seasons.Stages )
                {
                    // Get the schedule of the sport season
                    var schedule = await service.GetSeasonScheduleAsync( season.Id );
                    if ( schedule == null || season.Scheduled.Year != DateTime.Now.Year )
                    {
                        // Skip unscheduled or past/future seasons
                        continue;
                    }

                    foreach ( var tournament in schedule.Stages )
                    {
                        var tournamentSchedule = await service.GetTournamentScheduleAsync(tournament.Id);
                        var tournamentInfo = await service.GetTournamentInfoAsync(tournament.Id);

                        foreach ( var race in tournament.Stages )
                        {
                            // Copy race data
                            var dbRace = new Race()
                            {
                                Name = race.Name,
                                Description = race.Description,
                                StartDate = race.Scheduled,
                                EndDate = race.ScheduledEnd
                            };
                            context.Races.Add( dbRace );
                            await context.SaveChangesAsync();

                            var summary = await service.GetSportEventSummaryAsync( race.Id );
                            var raceSummary = await service.GetRaceSummaryAsync(race.Id);
                            if ( race.Stages.Count > 0 )
                            {
                                foreach ( var stage in race.Stages )
                                {
                                    var stageSummary = await service.GetStageSummaryAsync( stage.Id );
                                    var dbStage = new Stage()
                                    {
                                        Name = stage.Description,
                                        RaceId = dbRace.Id,
                                    };

                                    context.Stages.Add( dbStage );
                                    await context.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                // Create placeholder stage to show in UI
                                var dbStage = new Stage()
                                {
                                    Name = "Stage 1",
                                    RaceId = dbRace.Id,
                                };

                                context.Stages.Add( dbStage );
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}
