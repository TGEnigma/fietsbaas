using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private string email;
        private int points;

        public string Email
        {
            get => email;
            set => SetProperty( ref email, value );
        }

        public int Points
        {
            get => points;
            set => SetProperty( ref points, value );
        }

        public Command ResetPasswordCommand { get; set; }
        public Command ResetEmailCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public ProfileViewModel()
        {
            Email = App.User.Email;
            Points = App.User.Points;
        }
    }
}
