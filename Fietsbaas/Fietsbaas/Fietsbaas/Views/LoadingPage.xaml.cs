using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fietsbaas.Views
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class LoadingPage : ContentPage
    {
        private Timer timer;
        private int labelTimer;

        public LoadingPage()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 100;
            timer.Elapsed += Timer_Elapsed;
            //timer.Start();
        }

        private void Timer_Elapsed( object sender, ElapsedEventArgs e )
        {
            labelTimer = ( labelTimer + 1 ) % 3;

            loadingLabel.Text = "Loading";
            for ( int i = 0; i < labelTimer; i++ )
            {
                loadingLabel.Text += ".";
            }
        }

        public void SetProgress(float progress)
        {
            progressBar.ProgressTo( progress, 500, Easing.Linear );
            if ( progress >= 1 )
            {
                timer.Stop();
            }
        }

        protected override void OnDisappearing()
        {
            
        }
    }
}