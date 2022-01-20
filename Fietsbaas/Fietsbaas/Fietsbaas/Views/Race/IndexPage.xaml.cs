using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Fietsbaas.ViewModels;
using Fietsbaas.Models;

namespace Fietsbaas.Views.Race
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class IndexPage : BasePage
    {
        private readonly RaceIndexViewModel _vm;

        public IndexPage()
        {
            InitializeComponent();
            BindingContext = _vm = new RaceIndexViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.OnAppearing();
            BindingContext = null;
            BindingContext = _vm;
        }
    }
}