﻿using Fietsbaas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fietsbaas.Views.Race.Team
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class IndexPage : ContentPage
    {
        private TeamIndexViewModel vm;

        public IndexPage()
        {
            InitializeComponent();
            BindingContext = vm = new TeamIndexViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.Refresh();
        }
    }
}