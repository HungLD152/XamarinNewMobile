using SmartNews.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class SettingView : ContentView
    {
        private RssItemViewModel viewModel = new RssItemViewModel();
        public SettingView()
        {
            InitializeComponent();
        }
        void OnSettingButtonPressed(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new SettingPage());
        }
        //void OnSearchButtonPressed(object sender, EventArgs args)
        //{
        //    viewModel.LoadRssFeed();
        //}
    }
}
