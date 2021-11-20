using Assignment3.Models;
using Assignment3.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment3
{
    public partial class App : Application
    {
        public static User globalUser { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
