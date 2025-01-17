﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public DbSet<TeamRacer> TeamRacers { get; set; }
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
                    Points = 20,
                    Role = Role.Admin,
                };
                context.Users.Add( adminUser );

                var testUser = new User()
                {
                    Email = "user@mail.com",
                    Password = "user",
                    Points = 5,
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

                var random = new Random();

                void AddCyclist(string name, params Race[] races)
                {
                    // Create cyclists
                    var cyclist = new Cyclist()
                    {
                        Name = name
                    };
                    context.Cyclists.Add( cyclist );
                    context.SaveChanges();

                    // Create racers
                    foreach ( var race in races )
                    {
                        var racer = new Racer()
                        {
                            Cyclist = cyclist,
                            Position = random.Next() % 10,
                            Race = race,
                            Status = RacerStatus.Finished,
                        };

                        context.Racers.Add( racer );
                    }

                    context.SaveChanges();
                }

                // Create cyclists
                AddCyclist( "Jan Smit", race1, race2 );
                AddCyclist( "Bert Jan", race1, race2 );
                AddCyclist("Martien Lenn", race1, race2);
                AddCyclist("Tim Nerman", race1, race2);
                AddCyclist("Oscar Felix", race1, race2);
                AddCyclist("Henning Roman", race1, race2);
                AddCyclist("Emil Leon", race1, race2);
                AddCyclist("Willy Florian", race1, race2);
                AddCyclist("Tomasso Lodewijk", race1, race2);
                AddCyclist("Wim Mads", race1, race2);


                // Create teams
                var teamRacer1 = new TeamRacer()
                {
                    IsReserve = false,
                    Racer = context.Racers.First(),
                    Bet = BetType.WinsAnyRace,
                };
                context.TeamRacers.Add( teamRacer1 );
                context.SaveChanges();

                var team1 = new Team()
                {
                    User = adminUser,
                    Race = race1,
                    Racers = new []
                    {
                        teamRacer1
                    }
                };
                context.Teams.Add( team1 );
                context.SaveChanges();

                var teamRacer2 = new TeamRacer()
                {
                    IsReserve = false,
                    Racer = context.Racers.First(),
                    Bet = BetType.WinsAnyRace,
                };
                context.TeamRacers.Add( teamRacer2 );
                context.SaveChanges();

                var team2 = new Team()
                {
                    User = adminUser,
                    Race = race2,
                    Racers = new[]
                    {
                        teamRacer2
                    }
                };
                context.Teams.Add( team2 );
                context.SaveChanges();

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
                                Name = race.Description ?? "",
                                Description = ( season.Name ?? tournament.Description ) ?? "",
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
