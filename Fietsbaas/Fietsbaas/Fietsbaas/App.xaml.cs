﻿using Fietsbaas.Models;
using Fietsbaas.Services;
using Fietsbaas.Views;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fietsbaas
{
    public partial class App : Application
    {
        public static User User { get; private set; }
        public static bool SkipLogin { get; set; } = false;

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<FietsbaasDbContext>();
            MainPage = new AppShell();
        }

        public static void Login( string email, string password )
        {
            using ( var context = new FietsbaasDbContext() )
            {
                var user = context.Users.Where( x => x.Email.ToLower() == email.ToLower() ).FirstOrDefault();
                if ( user == null || user.Password != password )
                    throw new ApplicationException( "Unknown combination of email and password" );

                User = user;
            }
        }

        protected override async void OnStart()
        {
            FietsbaasDbContext.DropAndSeed();
            var seedTask = FietsbaasDbContext.SeedWithSportradar();
            var loadingPage = new LoadingPage();
            await Shell.Current.Navigation.PushModalAsync( loadingPage );
            await seedTask;
            loadingPage.SetProgress( 0.5f );

            if ( SkipLogin )
            {
                App.Login( "admin@mail.com", "admin" );
            }

            loadingPage.SetProgress( 1f );
            await Shell.Current.Navigation.PopModalAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
