using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Fietsbaas.Services.Github
{
    public class GithubService
    {
        protected static async Task<T> GetJsonResponseAsync<T>( string uri, string token = null ) where T : class
        {
            using ( var client = new HttpClient() )
            {
                client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
                if ( token != null )
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "token", token );

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

        public async Task<string> GetAccessCodeAsync()
        {
            // get access token request code using web authenticator
            var clientId = Encoding.UTF8.GetString( Convert.FromBase64String( Resources.GithubClientId ) );
            var authResult = await WebAuthenticator.AuthenticateAsync(
                new Uri( $"https://github.com/login/oauth/authorize?scope=user&client_id={clientId}" ),
                new Uri( "myapp://" ) );

            return authResult.Properties[ "code" ];
        }

        public async Task<string> GetAccessTokenAsync( string accessCode )
        {
            var clientId = Encoding.UTF8.GetString( Convert.FromBase64String( Resources.GithubClientId ) );
            var clientSecret = Encoding.UTF8.GetString( Convert.FromBase64String( Resources.GithubClientSecret ) );
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
            var token = await GetJsonResponseAsync<Token>( $"https://github.com/login/oauth/access_token?client_id={clientId}&client_secret={clientSecret}&code={accessCode}" );
            return token.AccessToken;
        }

        public async Task<User> GetUserAsync( string accessToken )
        {
            return await GetJsonResponseAsync<User>( $"https://api.github.com/user", token: accessToken );
        }
    }

    // https://json2csharp.com/
    public class Plan
    {
        [JsonProperty( "name" )]
        public string Name { get; set; }

        [JsonProperty( "space" )]
        public int Space { get; set; }

        [JsonProperty( "private_repos" )]
        public int PrivateRepos { get; set; }

        [JsonProperty( "collaborators" )]
        public int Collaborators { get; set; }
    }

    public class User
    {
        [JsonProperty( "login" )]
        public string Login { get; set; }

        [JsonProperty( "id" )]
        public int Id { get; set; }

        [JsonProperty( "node_id" )]
        public string NodeId { get; set; }

        [JsonProperty( "avatar_url" )]
        public string AvatarUrl { get; set; }

        [JsonProperty( "gravatar_id" )]
        public string GravatarId { get; set; }

        [JsonProperty( "url" )]
        public string Url { get; set; }

        [JsonProperty( "html_url" )]
        public string HtmlUrl { get; set; }

        [JsonProperty( "followers_url" )]
        public string FollowersUrl { get; set; }

        [JsonProperty( "following_url" )]
        public string FollowingUrl { get; set; }

        [JsonProperty( "gists_url" )]
        public string GistsUrl { get; set; }

        [JsonProperty( "starred_url" )]
        public string StarredUrl { get; set; }

        [JsonProperty( "subscriptions_url" )]
        public string SubscriptionsUrl { get; set; }

        [JsonProperty( "organizations_url" )]
        public string OrganizationsUrl { get; set; }

        [JsonProperty( "repos_url" )]
        public string ReposUrl { get; set; }

        [JsonProperty( "events_url" )]
        public string EventsUrl { get; set; }

        [JsonProperty( "received_events_url" )]
        public string ReceivedEventsUrl { get; set; }

        [JsonProperty( "type" )]
        public string Type { get; set; }

        [JsonProperty( "site_admin" )]
        public bool SiteAdmin { get; set; }

        [JsonProperty( "name" )]
        public string Name { get; set; }

        [JsonProperty( "company" )]
        public string Company { get; set; }

        [JsonProperty( "blog" )]
        public string Blog { get; set; }

        [JsonProperty( "location" )]
        public string Location { get; set; }

        [JsonProperty( "email" )]
        public string Email { get; set; }

        [JsonProperty( "hireable" )]
        public bool? Hireable { get; set; }

        [JsonProperty( "bio" )]
        public string Bio { get; set; }

        [JsonProperty( "twitter_username" )]
        public string TwitterUsername { get; set; }

        [JsonProperty( "public_repos" )]
        public int PublicRepos { get; set; }

        [JsonProperty( "public_gists" )]
        public int PublicGists { get; set; }

        [JsonProperty( "followers" )]
        public int Followers { get; set; }

        [JsonProperty( "following" )]
        public int Following { get; set; }

        [JsonProperty( "created_at" )]
        public DateTime CreatedAt { get; set; }

        [JsonProperty( "updated_at" )]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty( "private_gists" )]
        public int PrivateGists { get; set; }

        [JsonProperty( "total_private_repos" )]
        public int TotalPrivateRepos { get; set; }

        [JsonProperty( "owned_private_repos" )]
        public int OwnedPrivateRepos { get; set; }

        [JsonProperty( "disk_usage" )]
        public int DiskUsage { get; set; }

        [JsonProperty( "collaborators" )]
        public int Collaborators { get; set; }

        [JsonProperty( "two_factor_authentication" )]
        public bool TwoFactorAuthentication { get; set; }

        [JsonProperty( "plan" )]
        public Plan Plan { get; set; }
    }


    public class Token
    {
        [JsonProperty( "access_token" )]
        public string AccessToken { get; set; }

        [JsonProperty( "scope" )]
        public string Scope { get; set; }

        [JsonProperty( "token_type" )]
        public string TokenType { get; set; }
    }
}
