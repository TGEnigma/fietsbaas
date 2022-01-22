using Fietsbaas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string email;
        private string password;
        private string passwordValidation;

        public string Email
        {
            get => email;
            set => SetProperty( ref email, value );
        }

        public string Password
        {
            get => password;
            set => SetProperty( ref password, value );
        }

        public string PasswordValidation
        {
            get => passwordValidation;
            set => SetProperty( ref passwordValidation, value );
        }

        public Command RegisterCommand { get; set; }

        public RegistrationViewModel()
        {
            RegisterCommand = new Command( OnRegisterClicked );
        }

        private async void OnRegisterClicked( object obj )
        {
            var user = Db.Users.Where( x => x.Email.ToLower() == email.ToLower() )
                .FirstOrDefault();

            if ( user != null )
            {
                await Shell.Current.DisplayAlert( "Error", "User with email already exists", "OK" );
                return;
            }

            user = new User()
            {
                Email = email,
                Password = password,
                Points = 0,
                Role = Role.User
            };
            Db.Users.Add( user );
            await Db.SaveChangesAsync();

            await Shell.Current.GoToAsync( "//login" );
        }
    }
}
