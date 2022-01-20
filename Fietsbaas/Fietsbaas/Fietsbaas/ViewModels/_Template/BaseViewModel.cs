using Fietsbaas.Models;
using Fietsbaas.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public FietsbaasDbContext Db => DependencyService.Get<FietsbaasDbContext>();

        bool isBusy = false;
        public bool IsRefreshing
        {
            get { return isBusy; }
            set { SetProperty( ref isBusy, value ); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty( ref title, value ); }
        }

        protected bool SetProperty<T>( ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null )
        {
            if ( EqualityComparer<T>.Default.Equals( backingStore, value ) )
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged( propertyName );
            return true;
        }

        public virtual void OnAppearing()
        {
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged( [CallerMemberName] string propertyName = "" )
        {
            var changed = PropertyChanged;
            if ( changed == null )
                return;

            changed.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }
        #endregion
    }
}
