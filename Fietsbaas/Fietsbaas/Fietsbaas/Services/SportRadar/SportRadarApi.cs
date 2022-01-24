using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fietsbaas.Services.SportRadar
{
    public class SportRadarApi
    {
        protected static readonly string language_code = "en";
        protected static readonly string format = "json";
        protected static readonly string your_api_key = Encoding.UTF8.GetString( Convert.FromBase64String( "ejZnZnJyODhlN3EzbWRmeWZ3NHI5ZTJt" ) );

        protected static async Task<T> GetJsonResponseAsync<T>( string uri ) where T : class
        {
            using ( var client = new HttpClient() )
            {
                try
                {
                    var json = await client.GetStringAsync( uri );
                    return JsonConvert.DeserializeObject<T>( json );
                }
                catch ( Exception ex )
                {
                    return null;
                }
            }
        }
    }
}
