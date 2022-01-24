using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fietsbaas.Services.SportRadar
{
    public class SportRadarService
    {
        private static readonly SportRadarApiV1 v1 = new SportRadarApiV1();
        private static readonly SportRadarApiV2 v2 = new SportRadarApiV2();

        public async Task<RaceSummary> GetRaceSummaryAsync( string race_id )
        {
            return await v1.GetRaceSummaryAsync( race_id );
        }

        public async Task<RiderProfile> GetRiderProfileAsync( string rider_id )
        {
            return await v1.GetRiderProfileAsync( rider_id );
        }

        public async Task<Schedule> GetScheduleAsync( int year, int month, int day )
        {
            return await v1.GetScheduleAsync( year, month, day );
        }

        public async Task<StageSummary> GetStageSummaryAsync( string stage_id )
        {
            return await v1.GetStageSummaryAsync( stage_id );
        }

        // not working
        public async Task<TournamentInfo> GetTournamentInfoAsync( string tournament_id )
        {
            return await v1.GetTournamentInfoAsync( tournament_id );
        }

        // not working
        public async Task<TournamentList> GetTournamentListAsync()
        {
            return await v1.GetTournamentListAsync();
        }

        public async Task<TournamentSchedule> GetTournamentScheduleAsync( string tournament_id )
        {
            return await v1.GetTournamentScheduleAsync( tournament_id );
        }

        // V2
        public async Task<TeamProfile> GetTeamProfileAsync( string team_id )
        {
            return await v2.GetTeamProfileAsync( team_id );
        }

        public async Task<CompetitorProfile> GetCompetitorProfileAsync( string competitor_id )
        {
            return await v2.GetCompetitorProfileAsync( competitor_id );
        }

        public async Task<RankingsResult> GetRankingsAsync()
        {
            return await v2.GetRankingsAsync();
        }

        public async Task<SeasonSchedule> GetSeasonScheduleAsync( string stage_id )
        {
            return await v2.GetSeasonScheduleAsync( stage_id );
        }

        public async Task<Seasons> GetSeasonsAsync()
        {
            return await v2.GetSeasonsAsync();
        }

        public async Task<SportEventSummary> GetSportEventSummaryAsync( string stage_id )
        {
            return await v2.GetSportEventSummaryAsync( stage_id );
        }
    }

    public class TeamProfile
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "team" )]
        public Team Team { get; set; }
    }

    public class SportEventSummary
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "stage" )]
        public Stage Stage { get; set; }
    }

    public class Seasons
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "stages" )]
        public List<Stage> Stages { get; } = new List<Stage>();
    }

    public class Sport
    {
        [JsonProperty( "id" )]
        public string Id { get; set; }

        [JsonProperty( "name" )]
        public string Name { get; set; }
    }

    public class Category
    {
        [JsonProperty( "id" )]
        public string Id { get; set; }

        [JsonProperty( "name" )]
        public string Name { get; set; }
    }

    public class Tournament
    {
        [JsonProperty( "id" )]
        public string Id { get; set; }

        [JsonProperty( "name" )]
        public string Name { get; set; }

        [JsonProperty( "scheduled" )]
        public string Scheduled { get; set; }

        [JsonProperty( "scheduled_end" )]
        public string ScheduledEnd { get; set; }

        [JsonProperty( "sport" )]
        public Sport Sport { get; set; }

        [JsonProperty( "category" )]
        public Category Category { get; set; }
    }

    public class Course
    {
        [JsonProperty( "distance" )]
        public string Distance { get; set; }

        [JsonProperty( "distance_unit" )]
        public string DistanceUnit { get; set; }

        [JsonProperty( "departure_city" )]
        public string DepartureCity { get; set; }

        [JsonProperty( "arrival_city" )]
        public string ArrivalCity { get; set; }
    }

    public class Stage
    {
        [JsonProperty( "id" )]
        public string Id { get; set; }

        [JsonProperty( "name" )]
        public string Name { get; set; }

        [JsonProperty( "course" )]
        public Course Course { get; set; }

        [JsonProperty( "description" )]
        public string Description { get; set; }

        [JsonProperty( "scheduled" )]
        public DateTime Scheduled { get; set; }

        [JsonProperty( "scheduled_end" )]
        public DateTime ScheduledEnd { get; set; }

        [JsonProperty( "type" )]
        public string Type { get; set; }

        [JsonProperty( "single_event" )]
        public bool SingleEvent { get; set; }

        [JsonProperty( "stages" )]
        public List<Stage> Stages { get; } = new List<Stage>();

        [JsonProperty( "gender" )]
        public string Gender { get; set; }

        [JsonProperty( "departure_city" )]
        public string DepartureCity { get; set; }

        [JsonProperty( "arrival_city" )]
        public string ArrivalCity { get; set; }

        [JsonProperty( "status" )]
        public string Status { get; set; }

        [JsonProperty( "classification" )]
        public string Classification { get; set; }

        [JsonProperty( "distance" )]
        public string Distance { get; set; }

        [JsonProperty( "distance_unit" )]
        public string DistanceUnit { get; set; }

        [JsonProperty( "parents" )]
        public List<Stage> Parents { get; } = new List<Stage>();

        [JsonProperty( "competitors" )]
        public List<Competitor> Competitors { get; } = new List<Competitor>();

        [JsonProperty( "teams" )]
        public List<Team> Teams { get; } = new List<Team>();
    }

    public class Race
    {
        [JsonProperty( "id" )]
        public string Id { get; set; }

        [JsonProperty( "name" )]
        public string Name { get; set; }

        [JsonProperty( "scheduled" )]
        public string Scheduled { get; set; }

        [JsonProperty( "scheduled_end" )]
        public string ScheduledEnd { get; set; }

        [JsonProperty( "single_event" )]
        public bool SingleEvent { get; set; }

        [JsonProperty( "tournament" )]
        public Tournament Tournament { get; set; }

        [JsonProperty( "course" )]
        public Course Course { get; set; }

        [JsonProperty( "stages" )]
        public List<Stage> Stages { get; } = new List<Stage>();
    }

    public class SportEventStatu
    {
        [JsonProperty( "status" )]
        public string Status { get; set; }
    }

    public class Competitor
    {
        [JsonProperty( "id" )]
        public string Id { get; set; }

        [JsonProperty( "name" )]
        public string Name { get; set; }

        [JsonProperty( "country_code" )]
        public string CountryCode { get; set; }

        [JsonProperty( "nationality" )]
        public string Nationality { get; set; }

        [JsonProperty( "abbreviation" )]
        public string Abbreviation { get; set; }

        [JsonProperty( "gender" )]
        public string Gender { get; set; }

        [JsonProperty( "result" )]
        public Result Result { get; set; }
    }

    public class CoverageInfo
    {
        [JsonProperty( "live_coverage" )]
        public bool LiveCoverage { get; set; }
    }

    public class Result
    {
        [JsonProperty( "time" )]
        public string Time { get; set; }

        [JsonProperty( "time_ranking" )]
        public int TimeRanking { get; set; }

        [JsonProperty( "sprint" )]
        public int Sprint { get; set; }

        [JsonProperty( "sprint_ranking" )]
        public int SprintRanking { get; set; }

        [JsonProperty( "climber" )]
        public int Climber { get; set; }

        [JsonProperty( "climber_ranking" )]
        public int ClimberRanking { get; set; }

        [JsonProperty( "competitor" )]
        public Competitor Competitor { get; set; }

        [JsonProperty( "young_rider" )]
        public string YoungRider { get; set; }

        [JsonProperty( "young_rider_ranking" )]
        public int? YoungRiderRanking { get; set; }

        [JsonProperty( "team_time" )]
        public string TeamTime { get; set; }

        [JsonProperty( "team_time_ranking" )]
        public int TeamTimeRanking { get; set; }
    }

    public class RaceSummary
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "race" )]
        public Race Race { get; set; }

        [JsonProperty( "sport_event_status" )]
        public List<SportEventStatu> SportEventStatus { get; } = new List<SportEventStatu>();

        [JsonProperty( "competitors" )]
        public List<Competitor> Competitors { get; } = new List<Competitor>();

        [JsonProperty( "coverage_info" )]
        public CoverageInfo CoverageInfo { get; set; }

        [JsonProperty( "results" )]
        public List<Result> Results { get; } = new List<Result>();
    }

    public class Team
    {
        [JsonProperty( "id" )]
        public string Id { get; set; }

        [JsonProperty( "name" )]
        public string Name { get; set; }

        [JsonProperty( "country_code" )]
        public string CountryCode { get; set; }

        [JsonProperty( "nationality" )]
        public string Nationality { get; set; }

        [JsonProperty( "gender" )]
        public string Gender { get; set; }

        [JsonProperty( "result" )]
        public Result Result { get; set; }
    }

    public class RiderProfile
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "competitor" )]
        public Competitor Competitor { get; set; }

        [JsonProperty( "teams" )]
        public List<Team> Teams { get; } = new List<Team>();
    }

    public class Schedule
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }
    }

    public class SportEventStatus
    {
        [JsonProperty( "status" )]
        public string Status { get; set; }
    }

    public class StageSummary
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "stage" )]
        public Stage Stage { get; set; }

        [JsonProperty( "sport_event_status" )]
        public SportEventStatus SportEventStatus { get; set; }

        [JsonProperty( "competitors" )]
        public List<Competitor> Competitors { get; } = new List<Competitor>();

        [JsonProperty( "coverage_info" )]
        public CoverageInfo CoverageInfo { get; set; }

        [JsonProperty( "results" )]
        public List<Result> Results { get; } = new List<Result>();
    }

    public class TournamentSchedule
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "races" )]
        public List<Race> Races { get; } = new List<Race>();
    }

    public class TournamentList
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "tournaments" )]
        public List<Tournament> Tournaments { get; } = new List<Tournament>();
    }

    public class TournamentInfo
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "message" )]
        public string Message { get; set; }
    }

    public class CompetitorProfile
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "team" )]
        public Team Team { get; set; }
    }

    public class CompetitorRanking
    {
        [JsonProperty( "rank" )]
        public int Rank { get; set; }

        [JsonProperty( "points" )]
        public double Points { get; set; }

        [JsonProperty( "ranking_movement" )]
        public int RankingMovement { get; set; }

        [JsonProperty( "competitor" )]
        public Competitor Competitor { get; set; }
    }

    public class Ranking
    {
        [JsonProperty( "type_id" )]
        public int TypeId { get; set; }

        [JsonProperty( "name" )]
        public string Name { get; set; }

        [JsonProperty( "year" )]
        public int Year { get; set; }

        [JsonProperty( "week" )]
        public int Week { get; set; }

        [JsonProperty( "competitor_rankings" )]
        public List<CompetitorRanking> CompetitorRankings { get; } = new List<CompetitorRanking>();
    }

    public class RankingsResult
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "rankings" )]
        public List<Ranking> Rankings { get; } = new List<Ranking>();
    }

    public class SeasonSchedule
    {
        [JsonProperty( "generated_at" )]
        public DateTime GeneratedAt { get; set; }

        [JsonProperty( "schema" )]
        public string Schema { get; set; }

        [JsonProperty( "stages" )]
        public List<Stage> Stages { get; } = new List<Stage>();
    }
}
