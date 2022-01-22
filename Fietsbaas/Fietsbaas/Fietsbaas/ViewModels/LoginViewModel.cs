using Fietsbaas.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }

        private string loginemail;

        private string loginpassword;

        public string LoginEmail
        {
            get => loginemail;
            set => SetProperty(ref loginemail, value);
        }

        public string LoginPassword
        {
            get =>  loginpassword;
            set => SetProperty(ref loginpassword, value);
        }


        public LoginViewModel()
        {
            LoginCommand = new Command( OnLoginClicked );
            RegisterCommand = new Command( OnRegisterClicked );
        }

        private async void OnLoginClicked( object obj )
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            App.Login(LoginEmail, LoginPassword);
            await Shell.Current.GoToAsync( $"//index" );
        }

        private async void OnRegisterClicked()
        {
            await Shell.Current.GoToAsync( "registration" );
        }
    }
}
