using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fietsbaas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordConfirmationPage : ContentPage
    {
        public ForgotPasswordConfirmationPage()
        {
            InitializeComponent();
        }

        private async void BacktoLoginButton_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new LoginPage());
        }
    }
}