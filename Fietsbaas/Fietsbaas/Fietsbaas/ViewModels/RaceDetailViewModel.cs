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

        public StageDetailViewModel()
        {
            Title = "Stage details";
        }

        protected override void OnLoad( int id )
        {
            var stage = Db.Stages.Find( id );
            if ( stage != null )
            {
                Name = stage.Name;
            }
        }
        public override void Refresh()
        {
            IsRefreshing = true;      
        }
    }
}
