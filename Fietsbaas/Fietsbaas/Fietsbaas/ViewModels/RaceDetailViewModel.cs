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
    public class RaceDetailViewModel : BaseDetailViewModel
    {
        private int _id;
        private string name;
        private string description;
        private string stageName; 
        private ObservableCollection<Stage> stages;
        private RaceStatus status;

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

        public string StageName
        {
            get => stageName;
            set => SetProperty( ref stageName, value );
        }

        public ObservableCollection<Stage> Stages
        {
            get => stages;
            set => SetProperty( ref stages, value );
        }

        public RaceStatus Status
        {
            get => status;
            set => SetProperty( ref status, value );
        }

        public Command TeamCommand { get; set; }
        public Command RefreshCommand { get; set; }
        public Command<Stage> ItemTapped { get; set; }

        public RaceDetailViewModel()
        {
            Title = "Stages";
            TeamCommand = new Command( ExecuteTeamCommand );
            RefreshCommand = new Command(async () => await ExecuteRefresh());
            ItemTapped = new Command<Stage>( ExecuteItemTamppedCommand );
        }

        private async void ExecuteItemTamppedCommand( Stage stage )
        {
            try
            {
                await Shell.Current.GoToAsync( $"race/stage/detail?id={stage.Id}" );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        async Task ExecuteRefresh()
        {
            IsRefreshing = true;

            try
            {
                await OnRefreshAsync();
            }
            catch (Exception ex)
            {
                HandleException( ex );
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        async Task OnRefreshAsync()
        {
            Stages = new ObservableCollection<Stage>(Db.Stages.Where(x => x.RaceId == Id));
        }

        private async void ExecuteTeamCommand( object obj )
        {
            try
            {
                await Shell.Current.GoToAsync( $"race/team/index?raceid={Id}" );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        protected override void OnLoad( int id )
        {
            var race = Db.Races.Find( id );
            if ( race != null )
            {
                Name = race.Name;
                Description = race.Description;
                Stages = new ObservableCollection<Stage>(Db.Stages.Where(x => x.RaceId == id ));
                Status = race.Status;
            }
        }
        public override void Refresh()
        {
            IsRefreshing = true;      
        }
    }
}
