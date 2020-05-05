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
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjUyMDk4QDMxMzgyZTMxMmUzME10Q1dMQ2RaREhNcHluU0RlR0tDdXkzV2pVcWVQSEc5WTNlbXZWN1dsUWc9");
            InitializeComponent();
            MainPage = new NavigationPage(new MainPageCustom());
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
