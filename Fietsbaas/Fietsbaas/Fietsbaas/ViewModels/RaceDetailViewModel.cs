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

        public Command TeamCommand { get; set; }
        public Command RefreshCommand { get; set; }

        public RaceDetailViewModel()
        {
            Title = "Stages";
            TeamCommand = new Command( ExecuteTeamCommand );
            RefreshCommand = new Command(async () => await ExecuteRefresh());
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
                Debug.WriteLine(ex);
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
            await Shell.Current.GoToAsync( $"race/team/index?raceid={Id}" );
        }

        protected override void OnLoad( int id )
        {
            var race = Db.Races.Find( id );
            if ( race != null )
            {
                Name = race.Name;
                Description = race.Description;
                Stages = new ObservableCollection<Stage>(Db.Stages.Where(x => x.RaceId == id ));
            }
        }
        public override void OnAppearing()
        {
            base.OnAppearing();
            IsRefreshing = true; 
            
        }
    }
}
