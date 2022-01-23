using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Fietsbaas.Models;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class RaceIndexViewModel : BaseListViewModel<Race>
    {
        public RaceIndexViewModel()
        {
            Title = "Browse races";
        }

        protected override async Task OnRefreshAsync()
        {
            Items = new ObservableCollection<Race>( await Db.Races.ToListAsync() );
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
