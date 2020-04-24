using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class SettingView : ContentView
    {
        public SettingView()
        {
            InitializeComponent();
        }
        void OnSettingButtonPressed(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new SettingPage());
        }
    }
}
