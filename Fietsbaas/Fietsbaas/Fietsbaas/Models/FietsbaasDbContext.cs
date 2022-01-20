using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Fietsbaas.Models
{
    public class FietsbaasDbContext : DbContext
    {
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Cyclist> Cyclists { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Racer> Racers { get; set; }
        public DbSet<RacerStageProgress> RacerStageProgress { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
