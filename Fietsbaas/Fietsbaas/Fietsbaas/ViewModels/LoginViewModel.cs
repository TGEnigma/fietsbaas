using Fietsbaas.Services.Github;
using Fietsbaas.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command ForgotPasswordCommand { get; }
        public Command LoginWithGithubCommand { get; }

        private string loginemail;

        private string loginpassword;

        public string LoginEmail
        {
            get => loginemail;
            set => SetProperty(ref loginemail, value);
        }

        public string LoginPassword
        {
            get => loginpassword;
            set => SetProperty(ref loginpassword, value);
        }


        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
            ForgotPasswordCommand = new Command(OnForgotPasswordClicked);
            LoginWithGithubCommand = new Command( OnLoginWithGithubCLicked );
        }

        /// <summary>
        /// Logs in with a Github user account
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ApplicationException"></exception>
        private async void OnLoginWithGithubCLicked( object obj )
        {
            try
            {
                var github = new GithubService();
                string code;

                try
                {
                    code = await github.GetAccessCodeAsync();
                    if ( code == null )
                    {
                        throw new ApplicationException( "Github login cancelled" );
                    }
                }
                catch ( Exception )
                {
                    throw new ApplicationException( "Github login cancelled" );
                }


                var token = await github.GetAccessTokenAsync( code );
                var user = await github.GetUserAsync( token );
                var username = user.Email ?? user.Login; // Github doesn't require emails to be public

                var registeredUser = Db.Users.Where( x => x.Email.ToLower() == username.ToLower() ).FirstOrDefault();
                if ( registeredUser == null )
                {
                    // Register new account if it doesn't already exist
                    registeredUser = new Models.User()
                    {
                        Email = username,
                        Password = user.Id.ToString(),
                        Points = 0,
                        Role = Models.Role.User
                    };
                    Db.Users.Add( registeredUser );
                    await Db.SaveChangesAsync();
                }

                // Login using email and github user id
                if ( registeredUser.Password != user.Id.ToString() )
                    throw new ApplicationException( "Unable to login with Github account." );

                App.Login( username, user.Id.ToString() );
                await Shell.Current.GoToAsync( $"//index" );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                App.Login( LoginEmail, LoginPassword );

                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync( $"//index" );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        private async void OnRegisterClicked()
        {
            try
            {
                await Shell.Current.GoToAsync( "registration" );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        private async void OnForgotPasswordClicked()
        {
            try
            {
                await Shell.Current.GoToAsync( "forgotpassword" );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }
    }
}
