using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                    // Read response from cache if it exists to prevent being rate limited
                    var encodedUri = GetStringHash( uri );
                    var fileName = MakeValidFileName( encodedUri ) + ".json";
                    var filePath = Path.Combine( System.Environment.GetFolderPath( System.Environment.SpecialFolder.Personal ), fileName );
                    string json;
                    if ( !File.Exists( filePath ) )
                    {
                        // Fetch request from server
                        json = await client.GetStringAsync( uri );
                        File.WriteAllText( filePath, json );
                    }
                    else
                    {
                        // Read locally cached file
                        json = File.ReadAllText( filePath );
                    }

                    return JsonConvert.DeserializeObject<T>( json, new JsonSerializerSettings()
                    {
                        MissingMemberHandling = MissingMemberHandling.Error
                    } ); ; ;
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

        private static string MakeValidFileName( string name )
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape( new string( System.IO.Path.GetInvalidFileNameChars() ) );
            string invalidRegStr = string.Format( @"([{0}]*\.+$)|([{0}]+)", invalidChars );
            return System.Text.RegularExpressions.Regex.Replace( name, invalidRegStr, "_" );
        }

        internal static string GetStringHash( string text )
        {
            if ( String.IsNullOrEmpty( text ) )
                return String.Empty;

            using ( var sha = new System.Security.Cryptography.MD5CryptoServiceProvider() )
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes( text );
                byte[] hash = sha.ComputeHash( textData );
                return BitConverter.ToString( hash ).Replace( "-", String.Empty );
            }
        }
    }
}
