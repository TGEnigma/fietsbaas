using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    [QueryProperty( nameof( Id ), "id" )]
    public abstract class BaseDetailViewModel : BaseViewModel
    {
        private int id;

        public int Id
        {
            get => id;
            set
            {
                SetProperty( ref id, value );
                OnLoad( id );
            }
        }

        protected abstract void OnLoad( int id );
    }
}
