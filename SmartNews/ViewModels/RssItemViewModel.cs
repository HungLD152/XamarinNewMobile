using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Input;
using System.Xml.Linq;
using SmartNews.Models;
using SmartNews.Utils;
using SmartNews.Views;
using Xamarin.Forms;

namespace SmartNews.ViewModels
{
    public class RssItemViewModel : BaseViewModel
    {

        public string Url { get; set; }
        public string Parameter { get; set; }
        public string searchText { get; set; }
        public ObservableCollection<RSSFeedItem> Items { get; set; } = new ObservableCollection<RSSFeedItem>();
        public ObservableCollection<TabBarItemModel> ItemTabBar => GetTabBarItemModel();
        bool isRefreshing;

        public RssItemViewModel()
        {
            RefreshCommand = new Command(
                execute: () =>
                {
                    LoadRssFeed();
                },
                canExecute: () =>
                {
                    return !IsRefreshing;
                });

        }

        public ObservableCollection<TabBarItemModel> GetTabBarItemModel()
        {
            var list = new List<TabBarItemModel>();
            list.Add(new TabBarItemModel()
            {
                TitleBar = "24h.com.vn",
                Url = "https://cdn.24h.com.vn/upload/rss/trangchu24h.rss",
                ItemColor = Color.LightCoral
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "tinhte.vn",
                Url = "https://tinhte.vn/rss",
                ItemColor = Color.Orange
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "thanhnien.vn",
                Url = "https://thanhnien.vn/rss/home.rss",
                ItemColor = Color.Turquoise
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "trithuc.vn",
                Url = "https://trithucvn.net/feed",
                ItemColor = Color.LightSkyBlue
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "dantri.com.vn",
                Url = "https://dantri.com.vn/trangchu.rss",
                ItemColor = Color.MediumOrchid
            });
         
            return list.ToObservableCollection();
        }

        #region property RefreshCommand
        public ICommand RefreshCommand { private set; get; }

        public bool IsRefreshing
        {
            set { SetProperty(ref isRefreshing, value); }
            get { return isRefreshing; }
        }
        #endregion
        #region Load LoadRssFeed url rss
        public void LoadRssFeed()
        {
            WebRequest request = WebRequest.Create(Url);
            request.BeginGetResponse((args) =>
            {
                // Download XML.
                Stream stream = request.EndGetResponse(args).GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string xml = reader.ReadToEnd();

                // Parse XML to extract data from RSS feed.
                XDocument doc = XDocument.Parse(xml);
                XElement rss = doc.Element(XName.Get("rss"));
                XElement channel = rss.Element(XName.Get("channel"));

                // Set Title property.
                Title = channel.Element(XName.Get("title")).Value;

                // Set Items property.
                List<RSSFeedItem> list =
                    channel.Elements(XName.Get("item")).Select((XElement element) =>
                    {
                        return new RSSFeedItem()
                        {
                            Title = element.Element(XName.Get("title")).Value,
                            Description = element.Element(XName.Get("description")).Value,
                            Link = element.Element(XName.Get("link")).Value,
                            PubDate = element.Element(XName.Get("pubDate")).Value,
                            Thumbnail = ""
                        };
                    }).ToList();
                list.Add(new RSSFeedItem()
                {
                    Title = "TopCách ly toàn xã hội từ 1/4 trên toàn quốc: Người dân cần tuân thủ những gì?",
                    Description = "Cách ly toàn xã hội từ 1/4 trên toàn quốc: Người dân cần tuân thủ những gì?",
                    Link = "https://www.24h.com.vn/tin-tuc-trong-ngay/cach-ly-toan-xa-hoi-tu-1-4-tren-toan-quoc-nguoi-dan-can-tuan-thu-nhung-gi-c46a1136809.html",
                    PubDate = "Wed, 01 Apr 2020 14:13:34 +0700",
                    Thumbnail = "https://gamek.mediacdn.vn/2017/smile-emojis-icon-facebook-funny-emotion-women-s-premium-long-sleeve-t-shirt-1500882676711.jpg"
                });
                list.Add(new RSSFeedItem()
                {
                    Title = "Top1Cách ly toàn xã hội từ 1/4 trên toàn quốc: Người dân cần tuân thủ những gì?",
                    Description = "Cách ly toàn xã hội từ 1/4 trên toàn quốc: Người dân cần tuân thủ những gì?",
                    Link = "https://www.24h.com.vn/tin-tuc-trong-ngay/cach-ly-toan-xa-hoi-tu-1-4-tren-toan-quoc-nguoi-dan-can-tuan-thu-nhung-gi-c46a1136809.html",
                    PubDate = "Wed, 01 Apr 2020 14:13:34 +0700",
                    Thumbnail = "https://gamek.mediacdn.vn/2017/smile-emojis-icon-facebook-funny-emotion-women-s-premium-long-sleeve-t-shirt-1500882676711.jpg"
                });
                var lstItem = list.OrderByDescending(s => s.PubDateTime).ToList();
                try
                {
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        Items = lstItem.Where(s => s.Title.ToLower().Contains(searchText.ToLower())).ToList().ToObservableCollection();
                    }
                    else
                    {
                        Items = lstItem.ToObservableCollection();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                // Set IsRefreshing to false to stop the 'wait' icon.
                IsRefreshing = false;
            }, null);
        }
        #endregion
    }
}
