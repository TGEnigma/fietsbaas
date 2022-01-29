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
        public static readonly string[] BetTypes = new[]
{
            "First",
            "Second",
            "Third",
            "Fourth",
            "None"
        };

        private int raceId;

        public int RaceId
        {
            get => raceId;
            set => SetProperty( ref raceId, value );
        }

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

                foreach ( var item in Items )
                {
                    var tr = team.Racers.Where( x => x.RacerId == item.RacerId ).FirstOrDefault();
                    if ( item.IsSelected && tr == null )
                    {
                        // Link racer to team if not already
                        var betIndex = Array.IndexOf( BetTypes, item.Bet );
                        if ( betIndex == -1 )
                            betIndex = 0;

                        tr = new TeamRacer()
                        {
                            Bet = (BetType)betIndex,
                            IsReserve = false,
                            RacerId = item.RacerId,
                            TeamId = team.Id
                        };
                        Db.Add( tr );
                    }
                    else if ( tr != null )
                    {
                        // Remove link to team
                        Db.Remove( tr );
                    }
                }

                Db.SaveChanges();
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

            var racers = Db.Racers
                .Where( x => x.RaceId == RaceId )
                .Include( x => x.Cyclist )
                .ToList();

            Items = new ObservableCollection<TeamRacerViewModel>(
                from r in racers
                join tr in team.Racers ?? new List<TeamRacer>() on r.Id equals tr.RacerId into rtrJoin
                from rtr in rtrJoin.DefaultIfEmpty()
                select new TeamRacerViewModel( r.Id, r.Cyclist.Name, rtr != null ? rtr.Bet : BetType.WinsAnyRace, rtr != null )
            );
        }
    }

    public class TeamRacerViewModel : BaseViewModel
    {
        private string bet;
        private bool isSelected;
        private string name;
        private int racerId;

        public int RacerId { get => racerId; set => SetProperty( ref racerId, value ); }
        public string Name { get => name; set => SetProperty( ref name, value ); }
        public bool IsSelected { get => isSelected; set => SetProperty( ref isSelected, value ); }
        public string Bet { get => bet; set => SetProperty( ref bet, value ); }
        public string[] BetTypes => TeamIndexViewModel.BetTypes;

        public TeamRacerViewModel( int racerId, string name, BetType bet, bool isSelected )
        {
            RacerId = racerId;
            Name = name;
            Bet = TeamIndexViewModel.BetTypes[(int)bet];
            IsSelected = isSelected;
        }
    }
}
