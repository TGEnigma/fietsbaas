using Fietsbaas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fietsbaas.ViewModels
{
    public class LeaderbordViewModel : BaseListViewModel<User>
    {
        protected override Task OnAddItemAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task OnItemSelectedAsync(User item)
        {
            throw new NotImplementedException();
        }

        protected override async Task OnRefreshAsync()
        {
            Items = new ObservableCollection<User>(await Db.Users.OrderBy(x => x.Points ).ToListAsync());
        }

        
    }
}
