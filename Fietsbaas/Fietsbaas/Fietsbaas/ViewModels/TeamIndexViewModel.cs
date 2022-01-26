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
            "First",
            "Second",
            "Third",
            "Fourth",
            "None"
        };

        public Command SaveCommand { get; set; }

        public TeamIndexViewModel()
        {
            SaveCommand = new Command( ExecuteSaveCommand );
        }

        private async void ExecuteSaveCommand( object obj )
        {
            try
            {
                var team = await Db.Teams
                    .Where( x => x.UserId == App.User.Id && x.RaceId == raceId )
                    .Include( x => x.Racers )
                    .ThenInclude( x => x.Racer )
                    .ThenInclude( x => x.Cyclist )
                    .FirstOrDefaultAsync();
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

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
            var userId = App.User.Id;
            var team = await Db.Teams
                .Where( x => x.UserId == userId && x.RaceId == raceId )
                .Include( x => x.Racers )
                .ThenInclude( x => x.Racer )
                .ThenInclude( x => x.Cyclist )
                .FirstOrDefaultAsync();

            if ( team == null )
            {
                // Create team
                team = new Team()
                {
                    RaceId = RaceId,
                    UserId = userId,
                };
                Db.Teams.Add( team );
                await Db.SaveChangesAsync();
            }


            Items = new ObservableCollection<TeamRacerViewModel>(
                Db.Racers
                .Where( x => x.RaceId == RaceId )
                .Include( x => x.Cyclist )
                .ToList()
                .Join(
                    team.Racers,
                    racer => racer.Id,
                    teamRacer => teamRacer.RacerId,
                    ( racer, teamRacer ) => new
                    {
                        Id = racer.Id,
                        Name = racer.Cyclist.Name,
                        Bet = teamRacer != null ? teamRacer.Bet : BetType.WinsAnyRace,
                        IsSelected = teamRacer != null,
                    } )
                .Select( x => new TeamRacerViewModel( x.Id, x.Name, x.Bet, BetTypes, x.IsSelected ) )
            );
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

        public TeamRacerViewModel( int id, string name, BetType bet, IReadOnlyList<string> betTypes, bool isSelected )
        {
            Id = id;
            Name = name;
            BetCommand = new Command( OnBetPressed );
            Bet = betTypes[ (int)bet ];
            IsSelected = isSelected;
            BetTypes = betTypes;
        }

        private void OnBetPressed( object obj )
        {
            //Shell.Current.Navigation.PushModalAsync( new BetPage() );
        }
    }
}
