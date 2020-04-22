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

        void OnBackButtonClicked(object sender, EventArgs args)
        {
            webViewDetail.GoBack();
        }

        protected override bool OnBackButtonPressed()
        {
            if (webViewDetail.CanGoBack)
            {
                webViewDetail.GoBack();
                return true;
            }
            else return base.OnBackButtonPressed();
        }

        private void OnForwardButton_Clicked(object sender, EventArgs e)
        {
            if (webViewDetail.CanGoForward)
            {
                webViewDetail.GoForward();
            }
        }

        void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            labelLoading.IsVisible = true;
        }

        void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            labelLoading.IsVisible = false;
        }
    }
}
