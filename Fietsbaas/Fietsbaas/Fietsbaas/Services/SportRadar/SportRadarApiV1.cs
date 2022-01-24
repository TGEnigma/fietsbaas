using System.Threading.Tasks;

namespace Fietsbaas.Services.SportRadar
{
    public class SportRadarApiV1 : SportRadarApi
    {
        private static readonly string access_level = "t";
        private static readonly string version = "1";

        public async Task<RaceSummary> GetRaceSummaryAsync( string race_id )
        {
            return await GetJsonResponseAsync<RaceSummary>( $"https://api.sportradar.us/cycling-{access_level}{version}/{language_code}/races/{race_id}/summary.{format}?api_key={your_api_key}" );
        }

        public async Task<RiderProfile> GetRiderProfileAsync( string rider_id )
        {
            return await GetJsonResponseAsync<RiderProfile>( $"https://api.sportradar.us/cycling-{access_level}{version}/{language_code}/riders/{rider_id}/profile.{format}?api_key={your_api_key}" );
        }

        public async Task<Schedule> GetScheduleAsync( int year, int month, int day )
        {
            return await GetJsonResponseAsync<Schedule>( $"https://api.sportradar.us/cycling-{access_level}{version}/{language_code}/schedules/{year:D4}-{month:D2}-{day:D2}/schedule.{format}?api_key={your_api_key}" );
        }

        public async Task<StageSummary> GetStageSummaryAsync( string stage_id )
        {
            return await GetJsonResponseAsync<StageSummary>( $"https://api.sportradar.us/cycling-{access_level}{version}/{language_code}/stages/{stage_id}/summary.{format}?api_key={your_api_key}" );
        }

        // not working
        public async Task<TournamentInfo> GetTournamentInfoAsync( string tournament_id )
        {
            return await GetJsonResponseAsync<TournamentInfo>( $"https://api.sportradar.us/cycling-{access_level}{version}/{language_code}/tournaments/{tournament_id}/info.{format}?api_key={your_api_key}" );
        }

        // not working
        public async Task<TournamentList> GetTournamentListAsync()
        {
            return await GetJsonResponseAsync<TournamentList>( $"https://api.sportradar.us/cycling-{access_level}{version}/{language_code}/tournaments.{format}?api_key={your_api_key}" );
        }

        public async Task<TournamentSchedule> GetTournamentScheduleAsync( string tournament_id )
        {
            return await GetJsonResponseAsync<TournamentSchedule>( $"https://api.sportradar.us/cycling-{access_level}{version}/{language_code}/tournaments/{tournament_id}/schedule.{format}?api_key={your_api_key}" );
        }
    }
}
