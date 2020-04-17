using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartNews.Services;
using SmartNews.Views;

namespace SmartNews
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new MainPageCustom();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
