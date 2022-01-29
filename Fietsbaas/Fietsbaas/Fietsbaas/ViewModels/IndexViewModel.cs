using Fietsbaas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class IndexViewModel : BaseListViewModel<Race>

    {
        public string Username { get; set; }
        public int Points { get; set; }

        protected override async Task OnAddItemAsync()
        {
            //throw new NotImplementedException();
        }

        protected override async Task OnItemSelectedAsync(Race item)
        {
            await Shell.Current.GoToAsync($"race/detail?id={item.Id}");
        }

        protected override async Task OnRefreshAsync()
        {
            Username = App.User?.Email;
            Points = App.User?.Points ?? 0;
            Items = new ObservableCollection<Race>( await
                Db.Races
                .Where( x => x.StartDate >= DateTime.Now )
                .Take( 5 )
                .OrderBy( x => x.StartDate )
                .ToListAsync() );
        }
    }
}
