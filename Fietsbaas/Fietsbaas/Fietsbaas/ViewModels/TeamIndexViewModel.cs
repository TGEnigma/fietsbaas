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
    [QueryProperty(nameof(RaceId), "raceid")]
    internal class TeamIndexViewModel : BaseListViewModel<TeamRacerViewModel>
    {
        private int raceId;

        public int RaceId
        {
            get => raceId;
            set => SetProperty( ref raceId, value );
        }

        public IReadOnlyList<string> BetTypes = new string[]
        {
            "In top 4",
            "First",
            "Second",
            "Third",
            "Fourth"
        };

        protected override async Task OnAddItemAsync()
        {
            //throw new NotImplementedException();
        }

        protected override async Task OnItemSelectedAsync( TeamRacerViewModel item )
        {
            //throw new NotImplementedException();
        }

        protected override async Task OnRefreshAsync()
        {
            var team = await Db.Teams
                .Where( x => /*x.UserId == App.User.Id && */x.RaceId == raceId )
                .Include( x => x.Racers )
                .ThenInclude( x => x.Racer )
                .ThenInclude( x => x.Cyclist )
                .FirstOrDefaultAsync();

            if ( team != null )
            {
                Items = new ObservableCollection<TeamRacerViewModel>(
                    team.Racers
                    .AsQueryable()
                    .Select( x => new TeamRacerViewModel( x.Id, x.Racer.Cyclist.Name ) {  BetTypes = BetTypes, Bet = BetTypes[0] } )
                );
            }
        }
    }

    public class TeamRacerViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Command BetCommand { get; set; }
        public bool IsSelected { get; set; }
        public string Bet { get; set; }
        public IReadOnlyList<string> BetTypes { get; set; }

        public TeamRacerViewModel( int id, string name )
        {
            Id = id;
            Name = name;
            BetCommand = new Command( OnBetPressed );
        }

        private void OnBetPressed( object obj )
        {
            //Shell.Current.Navigation.PushModalAsync( new BetPage() );
        }
    }
}
