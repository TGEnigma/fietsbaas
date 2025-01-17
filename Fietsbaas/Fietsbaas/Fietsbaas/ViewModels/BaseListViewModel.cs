﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public abstract class BaseListViewModel<T> : BaseViewModel
        where T : class
    {
        private T _selectedItem;
        private ObservableCollection<T> _items;

        public ObservableCollection<T> Items
        {
            get => _items;
            set => SetProperty( ref _items, value );
        }

        public Command RefreshCommand { get; }
        public Command AddItemCommand { get; }
        public Command<T> ItemTapped { get; }

        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty( ref _selectedItem, value );
                OnItemSelected( value );
            }
        }

        public BaseListViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<T>();
            RefreshCommand = new Command( async () => await ExecuteRefresh() );
            ItemTapped = new Command<T>( OnItemSelected );
            AddItemCommand = new Command( OnAddItem );
        }

        async Task ExecuteRefresh()
        {
            IsRefreshing = true;

            try
            {
                await OnRefreshAsync();
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        public override void Refresh()
        {
            IsRefreshing = true;
            SelectedItem = null;
        }

        private async void OnAddItem( object obj )
        {
            try
            {
                await OnAddItemAsync();
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        async void OnItemSelected( T item )
        {
            if ( item == null )
                return;

            try
            {
                await OnItemSelectedAsync( item );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        protected abstract Task OnItemSelectedAsync( T item );
        protected abstract Task OnAddItemAsync();
        protected abstract Task OnRefreshAsync();
    }
}
