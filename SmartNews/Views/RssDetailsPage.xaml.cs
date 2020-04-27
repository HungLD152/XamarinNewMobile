using System;
using System.Collections.Generic;
using SmartNews.ViewModels;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class RssDetailsPage : ContentPage
    {
        public string Link { get; set; }
        public RssDetailsPage(string url)
        {
            InitializeComponent();
            webViewDetail.Source = url;
        }

        async void OnBackButtonClicked(object sender, EventArgs args)
        {
            if (webViewDetail.CanGoBack)
            {
                webViewDetail.GoBack();
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        private void OnForwardButton_Clicked(object sender, EventArgs e)
        {
            if (webViewDetail.CanGoForward)
            {
                webViewDetail.GoForward();
            }
        }

        private void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            webViewDetail.Reload();
        }

        void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            labelLoading.IsVisible = false;
        }

        void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            labelLoading.IsVisible = true;
        }
    }
}
