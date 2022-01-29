using Fietsbaas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class StageDetailViewModel : BaseDetailViewModel
    {
        private string name;
        private string description;
        private ObservableCollection<User> items;

        public string Name 
        { 
            get => name; 
            set => SetProperty( ref name, value ); 
        }

        public string Description
        { 
            get => description; 
            set => SetProperty( ref description, value );
        }

        public ObservableCollection<User> Items
        {
            get => items;
            set => SetProperty( ref items, value );
        }

        public Command RefreshCommand { get; }

        public StageDetailViewModel()
        {
            Title = "Stage details";
            RefreshCommand = new Command( async () => await ExecuteRefresh() );
        }

        async Task ExecuteRefresh()
        {
            IsRefreshing = true;

            try
            {
                //await OnRefreshAsync();
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        protected override void OnLoad( int id )
        {
            var stage = Db.Stages.Find( id );
            if ( stage != null )
            {
                Name = stage.Name;

                var userRacerScores = (from u in Db.Users
                                      join t in Db.Teams on u.Id equals t.UserId
                                      join tr in Db.TeamRacers on t.Id equals tr.TeamId
                                      join r in Db.Racers on tr.RacerId equals r.Id
                                      where t.RaceId == id && r.Position != null
                                      select new { User = u, Points = r.Position }).ToList();

                Items = new ObservableCollection<User>(
                    userRacerScores.ToList()
                    .GroupBy( x => x.User )
                    .Select( x => new User() { Email = x.Key.Email, Points = x.Sum( y => y.Points.Value ) } )
                );
            }
        }
        public override void Refresh()
        {
            IsRefreshing = true;      
        }
    }
}
