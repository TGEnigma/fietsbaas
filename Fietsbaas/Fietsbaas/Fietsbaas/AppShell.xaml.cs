using Fietsbaas.ViewModels;
using Fietsbaas.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Fietsbaas
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute( nameof( ItemDetailPage ), typeof( ItemDetailPage ) );
            Routing.RegisterRoute( nameof( NewItemPage ), typeof( NewItemPage ) );
            RegisterRoutes();
            CurrentItem = loginPage;
        }

        /// <summary>
        /// Register routes based on naming conventions.
        /// Sub-namespaces of Views are separated by a slash in their route name.
        /// 'Page' suffix is stripped.
        /// EG. Race/IndexPage.xaml -> race/index
        /// </summary>
        private void RegisterRoutes()
        {
            var viewTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where( x => x.Namespace?.StartsWith( "Fietsbaas.Views" ) ?? false )
                .Where( x => !x.Name.Contains( "<" ) );

            foreach ( var viewType in viewTypes )
            {
                var routeName =
                    viewType.FullName.Replace( "Fietsbaas.Views.", "" )
                    .Replace( ".", "/" );

                if ( routeName.EndsWith( "Page" ) )
                    routeName = routeName.Replace( "Page", "" );

                routeName = routeName.ToLower();
                Routing.RegisterRoute( routeName, viewType );
                Debug.WriteLine( $"Route '{routeName}' -> {viewType}" );
            }
        }

        private async void OnMenuItemClicked( object sender, EventArgs e )
        {
            await Shell.Current.GoToAsync( "//LoginPage" );
        }
    }
}
