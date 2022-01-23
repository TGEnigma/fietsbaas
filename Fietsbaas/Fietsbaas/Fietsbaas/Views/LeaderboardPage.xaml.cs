using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fietsbaas.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fietsbaas.ViewModels;

namespace Fietsbaas.Views
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class LeaderboardPage : ContentPage
    {
        private readonly LeaderbordViewModel _lb;
        public LeaderboardPage()
        {
            InitializeComponent();
            BindingContext = _lb = new LeaderbordViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _lb.OnAppearing();
            BindingContext = null;
            BindingContext = _lb;
        }

    }
    
    
}