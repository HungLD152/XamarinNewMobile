﻿using System;
using System.Collections.Generic;
using SmartNews.Models;
using SmartNews.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SmartNews.Views
{
    public partial class MainPageCustom : ContentPage
    {
        private RssItemViewModel viewModel = new RssItemViewModel();
        RSSFeedItem rssItem;
        public MainPageCustom()
        {
            InitializeComponent();
            rssItem = new RSSFeedItem();
            var setting = new SettingPage();
            var settingTabbar = new SettingTabView();
            viewModel.Url = "https://cdn.24h.com.vn/upload/rss/trangchu24h.rss";
            viewModel.LoadRssFeed();
            BindingContext = viewModel;
            setting.UpdateStyleItem();
            settingTabbar.UpdateSettingItem();
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("TabItem"))
            {
                if (Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["TabItem"].ToString()))
                {
                    TabBar.IsVisible = false;
                }
                else
                {
                    TabBar.IsVisible = true;
                }
            }
            TabBar.OnTabBarClicked += TabBar_OnTabItemClicked;
        }

        private void TabBar_OnTabItemClicked(object sender, string e)
        {
            viewModel.Url = e;
            viewModel.LoadRssFeed();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                // Deselect item.
                ((Xamarin.Forms.ListView)sender).SelectedItem = null;

                // Set WebView source to RSS item
                rssItem = (RSSFeedItem)args.SelectedItem;

                // For iOS 9, a NSAppTransportSecurity key was added to 
                //  Info.plist to allow accesses to EarthObservatory.nasa.gov sites.
                //webView.Source = rssItem.Link;

                await Navigation.PushAsync(new RssDetailsPage(rssItem.Link));
                // Hide and make visible.
                //ShowData.IsVisible = false;
                //webLayout.IsVisible = true;
            }
        }

        void OnSearchButtonPressed(object sender, EventArgs args)
        {
            viewModel.LoadRssFeed();
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            viewModel.LoadRssFeed();
        }

        void OnSettingButtonPressed(object sender, EventArgs args)
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SettingPage());
        }
    }
}

