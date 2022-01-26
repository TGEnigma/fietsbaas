using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fietsbaas.Services.SportRadar
{
    public abstract class SportRadarApi
    {
        protected static readonly string language_code = "en";
        protected static readonly string format = "json";
        protected static readonly string your_api_key = Encoding.UTF8.GetString( Convert.FromBase64String( Resources.SportRadarApiKey ) );

        protected static async Task<T> GetJsonResponseAsync<T>( string uri ) where T : class
        {
            using ( var client = new HttpClient() )
            {
                try
                {
                    var json = await client.GetStringAsync( uri );
                    Debug.WriteLine(json);
                    return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
                    {
                        MissingMemberHandling = MissingMemberHandling.Error
                    }); ; ;
                }
                catch ( HttpRequestException ex )
                {
                    return null;
                }
                catch ( Exception ex )
                {
                    return null;
                }
            }
        }
    }
}
