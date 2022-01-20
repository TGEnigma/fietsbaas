using Fietsbaas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class RaceDetailViewModel : BaseDetailViewModel
    {
        private string name;
        private string description;

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

        public RaceDetailViewModel()
        {
        }

        protected override void OnLoad( int id )
        {
            var race = Db.Races.Find( id );
            if ( race != null )
            {
                Name = race.Name;
                Description = race.Description;
            }
        }
    }
}
