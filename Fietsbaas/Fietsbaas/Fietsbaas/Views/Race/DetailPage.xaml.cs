using Fietsbaas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fietsbaas.Views.Race
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class DetailPage : ContentPage
    {
        private readonly RaceDetailViewModel _vm;
        public DetailPage()
        {
            InitializeComponent();
            BindingContext = _vm = new RaceDetailViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.OnAppearing();
        }
    }
}