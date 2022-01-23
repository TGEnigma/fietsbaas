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
            Items = new ObservableCollection<Race>(await Db.Races.OrderBy(x => x.StartDate).ToListAsync());
        }
    }
}
