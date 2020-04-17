using System;
namespace SmartNews.Models
{
    public partial class RSSFeedItem : BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string PubDate { get; set; }
        public string AuthorEmail { get; set; }
        public string Author { get; set; }
        public string Thumbnail { get; set; }
        public bool IsPriority { get; set; }
        public RSSFeedItem()
        {
        }
    }
    public partial class RSSFeedItem
    {
        public DateTime PubDateTime => DateTime.Parse(PubDate);
        public string DecoPubDate
        {
            get
            {
                var date = DateTime.Now - PubDateTime;
                if (date.TotalDays >= 1)
                    return date.Days.ToString() + " ngày trước";
                if (date.TotalHours >= 1)
                    return date.Hours.ToString() + " giờ trước";
                if (date.TotalMinutes >= 1)
                    return date.Minutes.ToString() + " phút trước";
                else
                    return date.Seconds.ToString() + " s trước";
            }
        }
    }
}
