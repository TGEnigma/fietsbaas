using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace Fietsbaas.ViewModels
{
    internal class ForgotPasswordViewModel : BaseViewModel
    {
        private string forgotpasswordemail;


        public Command ForgotPasswordCommand { get; }

        public ForgotPasswordViewModel()
        {
            ForgotPasswordCommand = new Command(OnForgotPasswordClicked);
        }

        public string ForgotPasswordEmail
        {
            get => forgotpasswordemail;
            set => SetProperty(ref forgotpasswordemail, value);
        }

   

        private async void OnForgotPasswordClicked()
        {
            try
            {
                await Shell.Current.GoToAsync( "forgotpasswordconfirmation" );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

    }
}
