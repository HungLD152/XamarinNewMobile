using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Xml;
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
                TitleBar = "cafebiz-cn",
                Url = "https://cafebiz.vn/cong-nghe.rss",
                ItemColor = Color.MediumOrchid
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "cafebiz-kd",
                Url = "https://cafebiz.vn/cau-chuyen-kinh-doanh.rss",
                ItemColor = Color.LightCoral
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "vnreview.vn",
                Url = "https://vnreview.vn/feed/-/rss/home",
                ItemColor = Color.LightSkyBlue
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "soha.vn",
                Url = "https://soha.vn/kinh-doanh.rss",
                ItemColor = Color.Orange
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "dantri.com.vn",
                Url = "https://dantri.com.vn/trangchu.rss",
                ItemColor = Color.Turquoise
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
            if (!string.IsNullOrEmpty(Url))
            {
                WebRequest request = WebRequest.Create(Url);
                request.BeginGetResponse((args) =>
                {
                    try
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
                            var desciption = element.Element(XName.Get("description"));
                            //var image = desciption.Element(XName.Get("img")).Attribute("src").Value.ToString();
                            var result = new RSSFeedItem();
                            result.Title = element.Element(XName.Get("title")).Value;
                            result.Description = desciption.Value;
                            result.Link = element.Element(XName.Get("link")).Value;
                            result.PubDate = element.Element(XName.Get("pubDate")).Value;
                            #region get images form description
                            Regex regx = new Regex("http(s?)://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?.(?:jpg|bmp|gif|png)", RegexOptions.IgnoreCase);
                            MatchCollection mactches = regx.Matches(desciption.ToString());
                            if (mactches.Count > 0)
                            {
                                foreach (var urlImage in mactches)
                                {
                                    result.Thumbnail = urlImage.ToString();
                                }
                            }
                            else
                            {
                                result.Thumbnail = "";
                            }
                            #endregion
                            return result;

                        }).ToList();
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
                    }
                    catch (Exception)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current.MainPage.DisplayAlert("Server Error", "Not Connected", "OK");
                        });
                    }
                }, null);
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Please check url no empty", "OK");
                });
            }


        }
    }
    #endregion
}
