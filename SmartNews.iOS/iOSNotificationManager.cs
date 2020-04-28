using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SmartNews.Models;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

[assembly: Dependency(typeof(SmartNews.iOS.iOSNotificationManager))]
namespace SmartNews.iOS
{
    class iOSNotificationManager : INotificationManager
    {
        public void ReceiveNotification(string title, string message)
        {
            new iOSNotificationReceiver().RegisterNotification(title, message);
        }
    }
}