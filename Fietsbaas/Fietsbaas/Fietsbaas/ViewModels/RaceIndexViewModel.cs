using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Fietsbaas.Models;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class RaceIndexViewModel : BaseListViewModel<Race>
    {
        public RaceIndexViewModel()
        {
            Title = "Browse";
        }

        protected override async Task OnRefreshAsync()
        {
            Items = new ObservableCollection<Race>()
            {
                new Race() { StartDate = new DateTime(2022, 6, 1), Name = "Tour de France" },
                new Race() { StartDate = new DateTime(2022, 2, 14), Name = "28th Valley of the Sun Stage Race" },
            };
        }

        protected override async Task OnAddItemAsync()
        {
            await Shell.Current.GoToAsync( "race/create" );
        }

        protected override async Task OnItemSelectedAsync( Race item )
        {
            await Shell.Current.GoToAsync( $"race/detail?id={item.Id}" );
        }
    }
}
