using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    [QueryProperty( nameof( Id ), nameof( Id ) )]
    public class RaceDetailViewModel : BaseViewModel
    {
        private int id;

        public int Id 
        { 
            get => id; 
            set
            {
                SetProperty( ref id, value );
                Load();
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }

        private void Load()
        {
            Name = "Tour de France";
            Description = "Description here";
        }
    }
}
