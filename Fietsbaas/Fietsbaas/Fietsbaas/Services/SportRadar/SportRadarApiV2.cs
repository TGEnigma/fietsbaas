using System.Threading.Tasks;

namespace Fietsbaas.Services.SportRadar
{
    public class SportRadarApiV2 : SportRadarApi
    {
        private static readonly string access_level = "trial";
        private static readonly string version = "v2";

        public async Task<TeamProfile> GetTeamProfileAsync( string team_id )
        {
            return await GetJsonResponseAsync<TeamProfile>( $"https://api.sportradar.us/cycling-{access_level}{version}/{language_code}/teams/{team_id}/profile.{format}?api_key={your_api_key}" );
        }

        public async Task<CompetitorProfile> GetCompetitorProfileAsync( string competitor_id )
        {
            return await GetJsonResponseAsync<CompetitorProfile>( $"https://api.sportradar.us/cycling/{access_level}/{version}/{language_code}/competitors/{competitor_id}/profile.{format}?api_key={your_api_key}" );
        }

        public async Task<RankingsResult> GetRankingsAsync()
        {
            return await GetJsonResponseAsync<RankingsResult>( $"https://api.sportradar.us/cycling/{access_level}/{version}/{language_code}/rankings.{format}?api_key={your_api_key}" );
        }

        public async Task<SeasonSchedule> GetSeasonScheduleAsync( string stage_id )
        {
            return await GetJsonResponseAsync<SeasonSchedule>( $"https://api.sportradar.us/cycling/{access_level}/{version}/{language_code}/sport_events/{stage_id}/schedule.{format}?api_key={your_api_key}" );
        }

        public async Task<Seasons> GetSeasonsAsync()
        {
            return await GetJsonResponseAsync<Seasons>( $"https://api.sportradar.us/cycling/{access_level}/{version}/{language_code}/seasons.{format}?api_key={your_api_key}" );
        }

        public async Task<SportEventSummary> GetSportEventSummaryAsync( string stage_id )
        {
            return await GetJsonResponseAsync<SportEventSummary>( $"https://api.sportradar.us/cycling/{access_level}/{version}/{language_code}/sport_events/{stage_id}/summary.{format}?api_key={your_api_key}" );
        }
    }
}
