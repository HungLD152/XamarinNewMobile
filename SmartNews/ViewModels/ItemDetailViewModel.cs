using System;

using SmartNews.Models;

namespace SmartNews.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public RSSFeedItem RssItem { get; set; }
        public ItemDetailViewModel(RSSFeedItem item = null)
        {
            Title = item?.Link;
            RssItem = item;
        }
    }
}
