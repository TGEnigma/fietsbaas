using Fietsbaas.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Fietsbaas.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}