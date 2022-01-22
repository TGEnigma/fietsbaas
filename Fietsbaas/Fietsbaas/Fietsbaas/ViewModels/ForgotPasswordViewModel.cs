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
            await Shell.Current.GoToAsync("forgotpasswordconfirmation");
        }

    }
}
