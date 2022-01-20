using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Fietsbaas.Models;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class RaceIndexViewModel : BaseViewModel
    {
        private Race _selectedItem;

        public ObservableCollection<Race> Items { get; set; }
        public Command RefreshCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Race> ItemTapped { get; }

        public Race SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty( ref _selectedItem, value );
                OnItemSelected( value );
            }
        }

        public RaceIndexViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Race>();
            RefreshCommand = new Command( async () => await OnRefresh() );
            ItemTapped = new Command<Race>( OnItemSelected );
            AddItemCommand = new Command( OnAddItem );
        }

        async Task OnRefresh()
        {
            IsRefreshing = true;

            try
            {
                Items = new ObservableCollection<Race>()
                {
                    new Race() { StartDate = new DateTime(2022, 6, 1), Name = "Tour de France" },
                    new Race() { StartDate = new DateTime(2022, 2, 14), Name = "28th Valley of the Sun Stage Race" },
                };
            }
            catch ( Exception ex )
            {
                Debug.WriteLine( ex );
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        public void OnAppearing()
        {
            IsRefreshing = true;
            SelectedItem = null;
        }

        private async void OnAddItem( object obj )
        {
            await Shell.Current.GoToAsync( "race/create" );
        }

        async void OnItemSelected( Race item )
        {
            if ( item == null )
                return;

            await Shell.Current.GoToAsync( $"race/detail?id={item.Id}" );
        }
    }
}
