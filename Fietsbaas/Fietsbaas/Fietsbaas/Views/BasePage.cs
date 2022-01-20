
using Xamarin.Forms;

using Fietsbaas.ViewModels;
using Fietsbaas.Models;

namespace Fietsbaas.Views
{
    public abstract class BasePage : ContentPage 
    {
        public BasePage()
        {
            HideAdminButtons();
        }

        private void HideAdminButtons()
        {
            var i = 0;
            while ( i < ToolbarItems.Count )
            {
                if ( ToolbarItems[ i ].ClassId == "admin" && App.User.Role != Role.Admin )
                {
                    ToolbarItems.RemoveAt( i );
                    i--;
                }

                i++;
            }
        }
    }
}