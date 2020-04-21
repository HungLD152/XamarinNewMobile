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
    }
}
