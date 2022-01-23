using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fietsbaas.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fietsbaas.Views
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class IndexPage : ContentPage
    {
      
        private readonly IndexViewModel _ip;

        public IndexPage()
        {
            InitializeComponent();
            BindingContext = _ip = new IndexViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ip.OnAppearing();
            BindingContext = null;
            BindingContext = _ip;
        }
    }
}